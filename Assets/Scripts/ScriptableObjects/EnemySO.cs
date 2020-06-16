using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
    public class EnemySO : ScriptableObject
    {
        #region Serialized Fields

        [SerializeField] private ElementType _element;
        [SerializeField] private ElementType _vulnerableTo;
        [SerializeField] private int _maxHealthPoints;
        [SerializeField] private float _speed;
        [SerializeField] private float _stoppingDistance;

        #endregion

        #region Properties

        public ElementType Element => _element;

        public int MaxHealthPoints
        {
            get => _maxHealthPoints;
            set => _maxHealthPoints = value;
        }

        public float Speed 
        {
            get => _speed;
            set => _speed = value;
        }

        public float StoppingDistance 
        { 
            get => _stoppingDistance; 
            set => _stoppingDistance = value; 
        }

        #endregion

        #region Methods

        public bool IsVulnerableTo(ElementType element) 
            => element == _vulnerableTo;

        #endregion
    }
}
