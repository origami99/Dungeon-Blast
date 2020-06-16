using Assets.Scripts.Behaviours;
using Assets.Scripts.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Pickups 
{
    public class AidKit : PickupBehaviour
    {
        [SerializeField] private AidKitSO _aidKitData;
        [SerializeField] private PlayerSO _playerData;
        [SerializeField] private float _floatingSpeed = 1;

        private Tween _floatingTween;

        private void Start()
        {
            _floatingTween = this.transform
                .DOMoveY(1, _floatingSpeed)
                .SetEase(Ease.InOutSine)
                .SetRelative(true)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public override void Collect()
        {
            RestorePercent();

            this.gameObject.SetActive(false);
        }

        private void RestorePercent()
        {
            int restorePercent = _aidKitData.CurrentTier.RegenerationPercent;
            float newHealth = _playerData.HealthPercent + restorePercent;
            _playerData.HealthPercent = Mathf.Clamp(newHealth, 0, 100);
        }

        private void OnDestroy() => _floatingTween.Kill();
    }
}
