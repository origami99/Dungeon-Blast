using Assets.Scripts.Common;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Shooting;
using System;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public static event Action<EnemyParams> OnDeath;
        public static event Action OnTakeDamage;

        [SerializeField] private EnemySO _enemyData;

        public EnemySO EnemyData
        {
            get => _enemyData;
            set => _enemyData = value;
        }

        public int HealthPoints { get; set; }

        private void Awake()
        {
            this.HealthPoints = _enemyData.MaxHealthPoints;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Bullet))
            {
                Weapon weapon = other.GetComponent<Bullet>().Weapon;
                ElementType element = weapon.WeaponData.Info.Element;

                if (this.EnemyData.IsVulnerableTo(element) &&
                    weapon.Holder.CompareTag(Tags.Player))
                {
                    TakeDamage(weapon.WeaponData.CurrentTier.Specs.Damage);
                }
            }
            else if (other.CompareTag(Tags.Melee))
            {
                TakeDamage(Combat.MELEE_DAMAGE);
            }
        }

        public void TakeDamage(int points)
        {
            HealthPoints -= points;
            OnTakeDamage?.Invoke();

            if (this.IsDead())
            {
                Die();
            }
        }

        private void Die()
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 5f);

            EnemyParams enemyParams = new EnemyParams
            {
                Position = this.transform.position
            };

            OnDeath?.Invoke(enemyParams);
        }

        public bool IsDead() => HealthPoints <= 0;
    }
}