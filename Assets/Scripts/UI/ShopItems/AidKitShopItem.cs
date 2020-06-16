using Assets.Scripts.Contracts;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.UI.Base;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class AidKitShopItem : ShopItem<AidKitTierSO>
    {
        [SerializeField] private AidKitSO _aidKit;

        protected override IItem<AidKitTierSO> Item
        {
            get
            {
                return _aidKit;
            }
            set
            {
                _aidKit = (AidKitSO)value;
            }
        }
    }
}
