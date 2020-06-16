using Assets.Scripts.Contracts;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.UI.Base;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [ExecuteInEditMode]
    public class WeaponShopItem : ShopItem<WeaponTierSO>
    {
        [SerializeField] private WeaponSO _weapon;

        protected override IItem<WeaponTierSO> Item
        {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = (WeaponSO)value;
            }
        }
    }
}