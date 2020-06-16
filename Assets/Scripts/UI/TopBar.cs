using Assets.Scripts.Pickups;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI
{
    public class TopBar : MonoBehaviour
    {
        [SerializeField] private PlayerSO _playerData;

        [SerializeField] private Slider _healthBar;
        [SerializeField] private TMP_Text _coinsText;

        private void Start()
        {
            _playerData.PropertyChanged += UpdateHUD;

            UpdateHealthBar();
            UpdateCoins();
        }

        private void UpdateHealthBar()
        {
            _healthBar.value = _playerData.HealthPercent;
        }

        private void UpdateCoins()
        {
            _coinsText.text = $"COINS: {_playerData.Coins}";
        }

        #region Event Listeners

        private void UpdateHUD(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_playerData.Coins):
                    UpdateCoins();
                    break;
                case nameof(_playerData.HealthPercent):
                    UpdateHealthBar();
                    break;
            }
        }

        #endregion
    }
}
