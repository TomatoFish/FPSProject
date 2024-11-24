using System;
using System.Collections.Generic;
using System.Linq;
using Code.Input;
using Code.Scripts.Models.Character.Player;
using UnityEngine;

namespace Code.Scripts.Character.Player.Guns
{
    public class GunsControlBehaviour : MonoBehaviour
    {
        [SerializeField] private InputController _input;
        [SerializeField] public List<GunSlotBehaviour> _gunSlots;

        private GunSlotBehaviour _selectedGunSlot;
        
        private void Start()
        {
            SelectGun(PlayerGunSlotType.Slot1);
            _input.GunSlot += SelectGun;
        }

        public void Enable(bool value)
        {
            gameObject.SetActive(value);
        }
        
        private void SelectGun(PlayerGunSlotType newType)
        {
            if (_selectedGunSlot != null && _selectedGunSlot.Type == newType) return;

            var newSelect = _gunSlots.FirstOrDefault(s => s.Type == newType);
            if (!newSelect.Available) return;

            foreach (var slot in _gunSlots)
            {
                slot.Enable(slot.Type == newType);
            }

            _selectedGunSlot = newSelect;
        }

        public bool UseGun()
        {
            return _selectedGunSlot.UseGun();
        }
    }
}