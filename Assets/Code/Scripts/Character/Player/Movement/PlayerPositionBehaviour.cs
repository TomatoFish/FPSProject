using System.Collections.Generic;
using Code.Scripts.Models.Character.Player;
using UnityEngine;

namespace Code.Scripts.Character.Player.Movement
{
    public class PlayerPositionBehaviour : MonoBehaviour, ICharacterColliderBehaviour
    {
        [SerializeField] private List<PlayerPositionState> _positionStates;

        private CapsuleCollider _currentCollider;
        private GameObject _currentCameraRoot;
        
        public float GetRadius => _currentCollider.radius;

        public float GetHeight => _currentCollider.height;

        public Transform CameraRoot => _currentCameraRoot.transform;
        
        private void Awake()
        {
            StateState((int)PlayerPosition.Stand);
        }

        public void StateState(int state)
        {
            foreach (var positionState in _positionStates)
            {
                var currentState = (int)positionState.State == state;
                
                positionState.EnableState(currentState);
                if (currentState)
                {
                    _currentCollider = positionState.Collider;
                    _currentCameraRoot = positionState.CameraTarget;
                }
            }
        }
    }
}