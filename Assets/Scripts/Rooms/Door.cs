using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Rooms
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform _openedPos;
        [SerializeField] private Transform _closedPos;

        [SerializeField] private Collider _hiddenCollider;
        [SerializeField] private GameObject _doorTrigger;

        private float _doorSpeed = 1.5f;
        private bool _isOpened = false;

        private Tween _tween;

        public void Open()
        {
            if (!_isOpened)
            {
                _tween = this.transform
                    .DOMove(_openedPos.position, _doorSpeed)
                    .SetEase(Ease.InOutQuad);

                _isOpened = true;
            }
        }

        public void Close()
        {
            if (_isOpened)
            {
                // Preventing player to pass while the door is closing
                _hiddenCollider.isTrigger = false;

                _tween = this.transform
                    .DOMove(_closedPos.position, _doorSpeed)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() => 
                    {
                        _hiddenCollider.isTrigger = true;
                    });

                _isOpened = false;
            }
        }

        public void EnableDoorTrigger() => _doorTrigger.SetActive(true);

        private void OnDestroy() => _tween.Kill();
    }
}
