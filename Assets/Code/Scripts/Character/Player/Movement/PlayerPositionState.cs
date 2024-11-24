using Code.Scripts.Models.Character.Player;
using UnityEngine;

namespace Code.Scripts.Character.Player.Movement
{
    public class PlayerPositionState : MonoBehaviour
    {
        [SerializeField] private PlayerPosition _state;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private GameObject _cameraTarget;

        public PlayerPosition State => _state;
        public CapsuleCollider Collider => _collider;
        public GameObject CameraTarget => _cameraTarget;

        public void EnableState(bool value)
        {
            _gameObject.SetActive(value);
            _cameraTarget.SetActive(value);
        }
    }
}