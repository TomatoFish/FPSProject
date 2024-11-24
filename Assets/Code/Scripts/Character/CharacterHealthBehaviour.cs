using System;
using Code.Scripts.Models.Common;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class CharacterHealthBehaviour : MonoBehaviour, IHealthBehaviour
    {
        [SerializeField] private float _maxHealth;

        public bool Invincible { get; private set; }
        public float CurrentHealth { get; private set; }

        private void Start()
        {
            Invincible = false;
            CurrentHealth = _maxHealth;
        }

        public void ApplyHeal(float value)
        {
            SetHealth(CurrentHealth + value);
        }
        
        public void ApplyDamage(float value)
        {
            if (Invincible) return;
            
            SetHealth(CurrentHealth - value);
        }

        public void SetInvincible(bool value)
        {
            Invincible = value;
        }
        
        private void SetHealth(float value)
        {
            CurrentHealth = Mathf.Min(value, _maxHealth);
        }
    }
}