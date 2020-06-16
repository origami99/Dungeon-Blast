using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player", menuName = "Data/Player")]
    public class PlayerSO : ScriptableObject, INotifyPropertyChanged
    {
        #region Serialized Fields

        [SerializeField] private float _healthPercent;
        [SerializeField] private int _coins;
        [SerializeField] private Vector3 _respawnPosition;

        #endregion

        #region Properties

        public float HealthPercent
        {
            get => _healthPercent;
            set
            {
                _healthPercent = value;
                OnPropertyChanged();
            }
        }

        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                OnPropertyChanged();
            }
        }

        public Vector3 RespawnPosition 
        { 
            get => _respawnPosition; 
            set => _respawnPosition = value; 
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
