using System;
using Code.Input;
using Code.Scripts.Models.Character.Player;
using UnityEngine;

namespace Code.Scripts.Character.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputController _input;
        [SerializeField] private PlayerPositionBehaviour _positionBehaviour;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _bodyRotationRoot;

        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _lookSpeedHorizontal = 1f;
        [SerializeField] private float _lookSpeedVertical = 1f;
        [SerializeField] private float _lookVerticalClampMax = 60f;
        [SerializeField] private float _lookVerticalClampMin = -60f;
        [SerializeField] private float _groundCheckDistance = 0.01f;
        [SerializeField] private float _slopeLimit = 40;
        [SerializeField] private float _jumpForce = 4f;
        [SerializeField] private float _landingResetTimer = 0.5f;
        [SerializeField] private LayerMask _groundMask;

        private Vector2 _moveRaw;
        private Vector2 _lookRaw;
        private bool _crouchRaw;
        private Vector3 _moveCache;
        private float _lookVerticalCache;
        private float _lookHorizontalCache;
        private float _jumpResetTimer;
        private bool _grounded;
        private bool _crouching;

        public void Start()
        {
            _input.Move += OnMoveInput;
            _input.Look += OnLookInput;
            _input.Jump += OnJumpInput;
            _input.Crouch += OnCrouchInput;
        }

        public void Update()
        {
            PerformHorizontalRotation();
            PerformVerticalLook();
            PerformCrouch();
            PerformMove(Time.deltaTime);
            CheckGrounded();

            if (_grounded) _jumpResetTimer += Time.deltaTime;
            else _jumpResetTimer = 0f;
        }

        public void OnEnable()
        {
            _input.EnableCharacterInput(true);
        }

        public void OnDisable()
        {
            _input.EnableCharacterInput(false);
        }

        private void OnMoveInput(Vector2 value)
        {
            _moveRaw = value;
        }

        private void OnLookInput(Vector2 value)
        {
            _lookRaw = value;
        }

        private void OnJumpInput()
        {
            if (!_grounded || _crouching || _jumpResetTimer < _landingResetTimer) return;

            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        private void OnCrouchInput(bool value)
        {
            _crouchRaw = value;
        }

        private void PerformMove(float deltaTime)
        {
            var movement = new Vector3(_moveRaw.x, 0f, _moveRaw.y) * (_moveSpeed * deltaTime);
            _moveCache = _grounded ? _bodyRotationRoot.TransformDirection(movement) : _moveCache;
            _rigidbody.Move(transform.position + _moveCache, Quaternion.identity);
        }

        private void PerformHorizontalRotation()
        {
            _lookHorizontalCache = _lookRaw.x * _lookSpeedHorizontal;
            _bodyRotationRoot.Rotate(new Vector3(0f, _lookHorizontalCache, 0f), Space.Self);
        }

        private void PerformVerticalLook()
        {
            _lookVerticalCache -= _lookRaw.y * _lookSpeedVertical;
            _lookVerticalCache = Math.Clamp(_lookVerticalCache, _lookVerticalClampMin, _lookVerticalClampMax);
            _positionBehaviour.CameraRoot.localEulerAngles = new Vector3(_lookVerticalCache, 0f, 0f);
        }

        private void PerformCrouch()
        {
            if (!_grounded || _crouching == _crouchRaw) return;

            _crouching = _crouchRaw;
            _positionBehaviour.StateState((int)(_crouching ? PlayerPosition.Crouch : PlayerPosition.Stand));
        }

        private void CheckGrounded()
        {
            if (Physics.CapsuleCast(GetCapsuleBottomHemisphere(), GetCapsuleTopHemisphere(),
                    _positionBehaviour.GetRadius, Vector3.down, out RaycastHit hit, _groundCheckDistance,
                    _groundMask, QueryTriggerInteraction.Ignore))
            {
                var groundNormal = hit.normal;
                _grounded = Vector3.Dot(hit.normal, transform.up) > 0f && IsNormalUnderSlopeLimit(groundNormal);
            }
            else
            {
                _grounded = false;
            }
        }



        // Gets the center point of the bottom hemisphere of the character controller capsule    
        Vector3 GetCapsuleBottomHemisphere()
        {
            return transform.position + (transform.up * (_positionBehaviour.GetRadius + _groundCheckDistance / 2));
        }

        // Gets the center point of the top hemisphere of the character controller capsule    
        Vector3 GetCapsuleTopHemisphere()
        {
            return transform.position + (transform.up * (_positionBehaviour.GetHeight - _positionBehaviour.GetRadius));
        }

        // Returns true if the slope angle represented by the given normal is under the slope angle limit of the character controller
        bool IsNormalUnderSlopeLimit(Vector3 normal)
        {
            return Vector3.Angle(transform.up, normal) <= _slopeLimit;
        }
    }
}