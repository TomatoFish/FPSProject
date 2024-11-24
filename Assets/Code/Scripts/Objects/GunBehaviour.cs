using UnityEngine;

namespace Code.Scripts.Objects
{
    public class GunBehaviour : MonoBehaviour
    {
        [SerializeField] protected Transform _muzzle;

        public void Enable(bool value)
        {
            gameObject.SetActive(value);
        }

        public virtual bool UseGun()
        {
            return false;
        }
    }
}