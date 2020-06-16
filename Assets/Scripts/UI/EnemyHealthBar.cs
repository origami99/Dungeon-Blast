using Assets.Scripts.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Slider _slider;

        private Camera _camera;

        private void Start()
        {
            Enemy.OnTakeDamage += UpdateHealthBar;

            _camera = Camera.main;
        }

        void Update()
        {
            this.transform.LookAt(
                transform.position + _camera.transform.rotation * -Vector3.back,
                _camera.transform.rotation * -Vector3.down);
        }

        private void UpdateHealthBar()
        {
            float percent = ((float)_enemy.HealthPoints / _enemy.EnemyData.MaxHealthPoints) * 100;

            _slider.value = percent;
        }

        private void OnDestroy() =>
            Enemy.OnTakeDamage -= UpdateHealthBar;
    }
}