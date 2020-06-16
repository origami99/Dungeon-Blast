using Assets.Scripts.Shooting;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private float _elapsedTime = 0;

        public Weapon Weapon 
        {
            get => _weapon;
            set => _weapon = value; 
        }

        private void Awake()
        {
            _weapon.Holder = this.gameObject;
        }

        private void Update()
        {
            float bulletSpawnTime = 1f / this.Weapon.WeaponData.CurrentTier.Specs.FireRate;

            if (Input.GetButton("Fire1") && _elapsedTime >= bulletSpawnTime)
            {
                this.Weapon.Shoot();

                _elapsedTime = 0;
            }

            _elapsedTime += Time.deltaTime;
        }
    }
}