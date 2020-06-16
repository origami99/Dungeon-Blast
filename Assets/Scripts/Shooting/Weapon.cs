using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [SerializeField] private WeaponSO _weaponData;

        [Header("Required Fields")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Transform _bulletContainer;

        public GameObject Holder { get; set; }

        public WeaponSO WeaponData
        {
            get => _weaponData;
            set => _weaponData = value;
        }

        public void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation, _bulletContainer);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(_firePoint.forward * this.WeaponData.CurrentTier.Specs.Force, ForceMode.Impulse);

            float bulletLifetime = this.WeaponData.CurrentTier.Specs.Range / this.WeaponData.CurrentTier.Specs.Force;
            Destroy(bullet, bulletLifetime);

            bullet.GetComponent<Bullet>().Weapon = this;
        }
    }
}
