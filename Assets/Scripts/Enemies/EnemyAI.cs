using Assets.Scripts.Shooting;
using UnityEngine;
using UnityEngine.AI;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private Enemy _enemy;
        private Transform _player;

        [SerializeReference]
        private float _shotElapsedTime = 0;

        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        private void Awake()
        {
            _enemy = this.GetComponent<Enemy>();
            _weapon.Holder = this.gameObject;

            _navMeshAgent.speed = _enemy.EnemyData.Speed;
            _navMeshAgent.stoppingDistance = _enemy.EnemyData.StoppingDistance;

            _player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        }

        private void Update()
        {
            float distance = Vector3.Distance(_enemy.transform.position, _player.position);

            if (distance <= Settings.ROOM_SIZE)
            {
                RotateToPlayer();
            }

            if (distance <= Settings.ROOM_SIZE * 0.8f)
            {
                _navMeshAgent.SetDestination(_player.position);

                ConstantShooting();
            }
        }

        private void RotateToPlayer()
        {
            Vector3 lookDirection = 2 * this.transform.position - _player.position;
            lookDirection = new Vector3(lookDirection.x, this.transform.position.y, lookDirection.z);
            this.transform.LookAt(lookDirection);
        }

        private void ConstantShooting()
        {
            float bulletSpawnTime = 1f / this.Weapon.WeaponData.CurrentTier.Specs.FireRate;

            if (_shotElapsedTime >= bulletSpawnTime)
            {
                this.Weapon.Shoot();

                _shotElapsedTime = 0;
            }

            _shotElapsedTime += Time.deltaTime;
        }
    }
}
