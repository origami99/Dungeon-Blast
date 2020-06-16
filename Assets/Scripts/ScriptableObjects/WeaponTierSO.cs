using Assets.Scripts.ScriptableObjects.Base;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponTier", menuName = "Data/Weapon/WeaponTier")]
    public class WeaponTierSO : ShopInfoSO
    {
        [SerializeField] private WeaponTierSpecsSO _specs;

        public WeaponTierSpecsSO Specs 
        { 
            get => _specs; 
            set => _specs = value; 
        }
    }
}
