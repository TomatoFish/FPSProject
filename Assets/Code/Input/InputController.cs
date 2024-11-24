using System;
using Code.Scripts.Models.Character.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Input
{
    public class InputController : MonoBehaviour
    {
        private DefaultInput _input;
    
        public Action<Vector2> Move;
        public Action<Vector2> Look;
        public Action Jump;
        public Action<bool> Crouch;
        public Action<bool> Fire;
        public Action<PlayerGunSlotType> GunSlot;
    
        public void Awake()
        {
            _input = new DefaultInput();
            _input.Character.Move.performed += MovementPerformedHandler;
            // _input.Character.Move.canceled += MovementCanceledHandler;
            _input.Character.Look.performed += LookPerformedHandler;
            // _input.Character.Look.canceled += LookCancelHandler;
            _input.Character.Jump.performed += JumpPerformedHandler;
            _input.Character.Crouch.started += CrouchPerformedHandler;
            _input.Character.Crouch.canceled += CrouchCanceledHandler;
            _input.Character.Fire.started += FirePerformedHandler;
            _input.Character.Fire.canceled += FireCanceledHandler;
            _input.Character.GunSlot1.performed += GunSlot1Handler;
            _input.Character.GunSlot2.performed += GunSlot2Handler;
            _input.Character.GunSlot3.performed += GunSlot3Handler;
            _input.Character.GunSlot4.performed += GunSlot4Handler;
        }

        public void EnableCharacterInput(bool value)
        {
            if (value)
                _input.Character.Enable();
            else
                _input.Character.Disable();
        }
        
        public void EnableUIInput(bool value)
        {
            if (value)
                _input.UI.Enable();
            else
                _input.UI.Disable();
        }
        
        private void MovementPerformedHandler(InputAction.CallbackContext context) =>
            Move?.Invoke(context.ReadValue<Vector2>());

        private void MovementCanceledHandler(InputAction.CallbackContext context) =>
            Move?.Invoke(Vector2.zero);
        
        private void LookPerformedHandler(InputAction.CallbackContext context) =>
            Look?.Invoke(context.ReadValue<Vector2>());

        private void LookCancelHandler(InputAction.CallbackContext context) =>
            Look?.Invoke(Vector2.zero);

        private void JumpPerformedHandler(InputAction.CallbackContext context) =>
            Jump?.Invoke();
        
        private void CrouchPerformedHandler(InputAction.CallbackContext context) =>
            Crouch?.Invoke(true);
        
        private void CrouchCanceledHandler(InputAction.CallbackContext context) =>
            Crouch?.Invoke(false);
        
        private void FirePerformedHandler(InputAction.CallbackContext context) =>
            Fire?.Invoke(true);
        
        private void FireCanceledHandler(InputAction.CallbackContext context) =>
            Fire?.Invoke(false);
        
        private void GunSlot1Handler(InputAction.CallbackContext context) =>
            GunSlot?.Invoke(PlayerGunSlotType.Slot1);
        
        private void GunSlot2Handler(InputAction.CallbackContext context) =>
            GunSlot?.Invoke(PlayerGunSlotType.Slot2);
        
        private void GunSlot3Handler(InputAction.CallbackContext context) =>
            GunSlot?.Invoke(PlayerGunSlotType.Slot3);
        
        private void GunSlot4Handler(InputAction.CallbackContext context) =>
            GunSlot?.Invoke(PlayerGunSlotType.Slot4);
    }
}