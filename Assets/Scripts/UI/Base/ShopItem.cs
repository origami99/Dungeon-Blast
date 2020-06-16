using Assets.Scripts.Contracts;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.ScriptableObjects.Base;
using Assets.Scripts.Shop;
using Assets.Scripts.Systems.Save;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Base
{
    public abstract class ShopItem<T> : MonoBehaviour
        where T : ShopInfoSO
    {
        [SerializeField] private string _id;

        [SerializeField] private PlayerSO _playerData;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private Button _buyButton;

        protected abstract IItem<T> Item { get; set; }

        protected PlayerSO PlayerData => _playerData;
        protected TMP_Text NameText => _nameText;
        protected TMP_Text PriceText => _priceText;
        protected TMP_Text ProgressText => _progressText;
        protected Slider ProgressSlider => _progressSlider;
        protected Button BuyButton => _buyButton;

        protected virtual void Awake()
        {
            BuyButton.onClick.AddListener(BuyNextUpgrade);
        }

        protected virtual void Start()
        {
            ShopCarpet.OnShopEnter += UpdateItem;
        }

#if UNITY_EDITOR
        protected virtual void Update() => UpdateItem();
#endif

        protected virtual void UpdateItem()
        {
            int currTierIndex = GetCurrTierIndex();

            this.NameText.SetText(this.Item.CurrentTier.UpgradeName);
            this.ProgressText.SetText($"Tier {currTierIndex} of {this.Item.Tiers.Count - 1}");

            this.ProgressSlider.maxValue = this.Item.Tiers.Count - 1;
            this.ProgressSlider.value = currTierIndex;

            bool isMaxUpgrade = currTierIndex >= this.Item.Tiers.Count - 1;
            bool canBuyUpgrade = !isMaxUpgrade && this.PlayerData.Coins >= GetNextTier().Price;
            this.BuyButton.interactable = canBuyUpgrade;

            this.PriceText.SetText(isMaxUpgrade ? "MAX" : $"Buy: {GetNextTier().Price}");
        }

        protected virtual void BuyNextUpgrade()
        {
            this.Item.CurrentTier = GetNextTier();
            this.PlayerData.Coins -= this.Item.CurrentTier.Price;
            UpdateItem();
        }

        #region Private Methods

        private int GetCurrTierIndex() => Array.IndexOf(this.Item.Tiers.ToArray(), this.Item.CurrentTier);

        private T GetNextTier() => GetTierByIndex(GetCurrTierIndex() + 1);

        private T GetTierByIndex(int index) => this.Item.Tiers.ToList()[index];

        #endregion

        #region Save / Load logic

        public void OnSave(DataSnapshot snapshot)
        {
            if (string.IsNullOrEmpty(_id)) return;

            ItemUpgrade upgrade = snapshot.ItemUpgrades.SingleOrDefault(x => x.ItemId == _id);

            var currUpgrade = new ItemUpgrade
            {
                ItemId = _id,
                Level = GetCurrTierIndex()
            };

            if (upgrade == null)
            {
                snapshot.ItemUpgrades.Add(currUpgrade);
            }
            else
            {
                upgrade.Level = currUpgrade.Level;
            }
        }

        public void OnLoad(DataSnapshot snapshot)
        {
            ItemUpgrade upgrade = snapshot.ItemUpgrades.SingleOrDefault(x => x.ItemId == _id);

            if (upgrade != null)
            {
                int tierIndex = upgrade.Level;
                this.Item.CurrentTier = GetTierByIndex(tierIndex);
            }
        }

        public void ResetItem(DataSnapshot snapshot)
        {
            ItemUpgrade upgrade = snapshot.ItemUpgrades.SingleOrDefault(x => x.ItemId == _id);

            if (upgrade != null)
            {
                upgrade.Level = 0;
            }

            this.Item.CurrentTier = GetTierByIndex(0);
        }

        #endregion
    }
}
