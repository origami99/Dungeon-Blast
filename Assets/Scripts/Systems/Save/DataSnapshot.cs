using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Systems.Save
{
    [Serializable]
    public class DataSnapshot
    {
        public DataSnapshot(Vector3 respawnPosition, float playerHealth, int coins) 
        {
            this.ItemUpgrades = new List<ItemUpgrade>();

            this.RespawnPosition = respawnPosition;
            this.PlayerHealth = playerHealth;
            this.Coins = coins;
        }

        [SerializeField] private Vector3 _respawnPosition;

        [SerializeField] private float _playerHealth;

        [SerializeField] private int _coins;

        [SerializeField] private List<ItemUpgrade> _itemUpgrades;

        public Vector3 RespawnPosition 
        {
            get => _respawnPosition; 
            set => _respawnPosition = value;
        }
        
        public float PlayerHealth 
        {
            get => _playerHealth; 
            set => _playerHealth = value;
        }

        public int Coins
        {
            get => _coins;
            set => _coins = value;
        }

        public List<ItemUpgrade> ItemUpgrades 
        {
            get => _itemUpgrades;
            set => _itemUpgrades = value;
        }
    }

    [Serializable]
    public class ItemUpgrade
    {
        [SerializeField] private string _itemId;

        [SerializeField] private int _level;

        public string ItemId 
        {
            get => _itemId; 
            set => _itemId = value; 
        }

        public int Level 
        {
            get => _level; 
            set => _level = value; 
        }
    }
}
