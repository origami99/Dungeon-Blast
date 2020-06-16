using Assets.Scripts.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AidKit", menuName = "Data/AidKit/AidKit")]
    public class AidKitSO : ScriptableObject, IItem<AidKitTierSO>
    {
        [SerializeField] private AidKitTierSO _currentTier;
        [SerializeField] private List<AidKitTierSO> _tiers;

        public AidKitTierSO CurrentTier 
        {
            get => _currentTier; 
            set => _currentTier = value; 
        }

        public ICollection<AidKitTierSO> Tiers 
        { 
            get => _tiers; 
            set => _tiers = value.ToList(); 
        }
    }
}
