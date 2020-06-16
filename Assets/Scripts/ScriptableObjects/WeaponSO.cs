using Assets.Scripts.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon/Weapon")]
    public class WeaponSO : ScriptableObject, IItem<WeaponTierSO>
    {
        [SerializeField] private WeaponInfoSO _info;

        [SerializeField] private WeaponTierSO _currentTier;

        [SerializeField] private List<WeaponTierSO> _tiers;

        public WeaponInfoSO Info
        {
            get => _info; 
            set => _info = value;
        }

        public WeaponTierSO CurrentTier 
        {
            get => _currentTier;
            set => _currentTier = value; 
        }

        public ICollection<WeaponTierSO> Tiers 
        { 
            get => _tiers; 
            set => _tiers = value.ToList(); 
        }
    }
}
