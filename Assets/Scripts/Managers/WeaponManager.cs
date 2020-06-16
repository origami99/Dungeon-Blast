using Assets.Scripts.Player;
using Assets.Scripts.Shooting;
using Assets.Scripts.Utils.Extenstions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Managers
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        
        private PlayerShooting _playerShooting;

        private LinkedList<Weapon> _weapons;
        private LinkedListNode<Weapon> _selectedWeapon;

        void Start()
        {
            _weapons = new LinkedList<Weapon>(
                _player.transform.FindObjectsWithTag(Tags.Weapon)
                    .Select(x => 
                    {
                        return x.GetComponent<Weapon>();
                    }));

            _playerShooting = _player.GetComponent<PlayerShooting>();

            _selectedWeapon = _weapons.Find(_playerShooting.Weapon);

            ActivateWeapon(_selectedWeapon.Value);
        }

        void Update()
        {
            bool isWeaponChanged = false;

            if (Input.GetKeyDown(KeyCode.E))
            {
                _selectedWeapon = _selectedWeapon.Next ?? _selectedWeapon.List.First;
                isWeaponChanged = true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _selectedWeapon = _selectedWeapon.Previous ?? _selectedWeapon.List.Last;
                isWeaponChanged = true;
            }

            if (isWeaponChanged)
            {
                Weapon weapon = _selectedWeapon.Value;

                _playerShooting.Weapon = weapon;
                ActivateWeapon(weapon);
            }
        }

        private void ActivateWeapon(Weapon weapon)
        {
            _weapons.ToList().ForEach(x => x.gameObject.SetActive(false));
            weapon.gameObject.SetActive(true);
            weapon.Holder = _player;
        }
    }
}
