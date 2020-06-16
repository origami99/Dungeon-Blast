using Assets.Scripts.Behaviours;
using Assets.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.Pickups
{
    public class Coin : PickupBehaviour
    {
        public static event Action OnCollect;

        [SerializeField] private PlayerSO _playerData;
        [SerializeField] private int _rotatinoSpeed = 100;

        void Update()
        {
            this.transform.Rotate(0, _rotatinoSpeed * Time.deltaTime, 0);
        }

        public override void Collect()
        {
            _playerData.Coins++;

            Destroy(this.gameObject);

            OnCollect?.Invoke();
        }

        #region Save / Load logic

        public void OnLoad(object _) => Destroy(this.gameObject);

        #endregion
    }
}