using Assets.Scripts.ScriptableObjects.Base;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AidKitTier", menuName = "Data/AidKit/AidKitTier")]
    public class AidKitTierSO : ShopInfoSO
    {
        [SerializeField] [Range(0, 100)] private int _regenerationPercent;

        public int RegenerationPercent 
        {
            get => _regenerationPercent; 
            set => _regenerationPercent = value;
        }
    }
}
