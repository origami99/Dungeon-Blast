using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponTierSpecs", menuName = "Data/Weapon/WeaponTierSpecs")]
    public class WeaponTierSpecsSO : ScriptableObject
    {
        #region Serialized Fields

        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _range;
        [SerializeField] private float _force;

        #endregion

        #region Properties

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float FireRate
        {
            get => _fireRate;
            set => _fireRate = value;
        }

        public float Range
        {
            get => _range;
            set => _range = value;
        }

        public float Force
        {
            get => _force;
            set => _force = value;
        }

        #endregion
    }
}
