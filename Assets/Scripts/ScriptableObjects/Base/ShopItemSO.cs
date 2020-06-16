using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Base
{
    public class ShopInfoSO : ScriptableObject
    {
        [Header("Tier Info")]
        [SerializeField] private string _tierName;
        [SerializeField] private int _price;

        public string UpgradeName 
        {
            get => _tierName;
            set => _tierName = value;
        }

        public int Price 
        { 
            get => _price; 
            set => _price = value; 
        }
    }
}
