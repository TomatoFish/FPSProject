using UnityEngine;

namespace Code.Scripts.Objects
{
    public class PistolBehaviour : GunBehaviour
    {
        [SerializeField] private GameObject _muzzleFlashPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _spread;
        
        public override bool UseGun()
        {
            
            
            return true;
        }
    }
}