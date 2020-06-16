using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Shooting;
using Assets.Scripts.Systems.Save;
using System;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Player
{

    public class Player : MonoBehaviour
    {
        public static event Action OnDeath;

        [SerializeField] private PlayerSO _playerData;
        [SerializeField] private PlayerMovement _playerMovement;

        public PlayerSO PlayerData => _playerData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Bullet))
            {
                Bullet bullet = other.GetComponent<Bullet>();
                Weapon weapon = bullet.Weapon;

                if (weapon.Holder.CompareTag(Tags.Enemy))
                {
                    TakeDamage(weapon.WeaponData.CurrentTier.Specs.Damage * Combat.DAMAGE_MULTIPLAYER);
                    Destroy(bullet.gameObject);
                }
            }
        }

        public void TakeDamage(int points)
        {
            this.PlayerData.HealthPercent -= points;

            if (this.PlayerData.HealthPercent < 0)
            {
                this.PlayerData.HealthPercent = 0;
            }

            if (this.IsDead())
            {
                OnDeath?.Invoke();
            }
        }

        public bool IsDead() => this.PlayerData.HealthPercent <= 0;

        #region Save / Load logic

        public void OnSave(DataSnapshot snapshot)
        {
            snapshot.PlayerHealth = _playerData.HealthPercent;
            snapshot.RespawnPosition = _playerData.RespawnPosition;
            snapshot.Coins = _playerData.Coins;
        }

        public void OnLoad(DataSnapshot snapshot)
        {
            _playerData.Coins = snapshot.Coins;
            _playerData.HealthPercent = snapshot.PlayerHealth;
            _playerData.RespawnPosition = snapshot.RespawnPosition;

            _playerMovement.Respawn();
        }

        #endregion
    }
}