using Code.Scripts.Models.Character.Player;
using Code.Scripts.Objects;
using UnityEngine;

namespace Code.Scripts.Character.Player.Guns
{
    public class GunSlotBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerGunSlotType _type;
        [SerializeField] private GunBehaviour gunBehaviour;

        public PlayerGunSlotType Type => _type;

        public bool Available => gunBehaviour != null;
        
        public void Enable(bool value)
        {
            if (gunBehaviour != null)
                gunBehaviour.Enable(value);
        }
        
        public bool UseGun()
        {
            return gunBehaviour.UseGun();
        }
    }
}
