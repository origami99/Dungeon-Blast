using Assets.Scripts.Enemies;
using Assets.Scripts.Pickups;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private Transform _coinsContainer;

        private void Start() => Enemy.OnDeath += SpawnCoin;
        private void OnDestroy() => Enemy.OnDeath -= SpawnCoin;

        public void SpawnCoin(EnemyParams enemyParams) =>
            Instantiate(_coinPrefab, enemyParams.Position, Quaternion.identity, _coinsContainer);
    }
}
