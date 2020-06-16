using Assets.Scripts.Rooms;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        [SerializeField] private float _smoothness = 0.125f;
        [SerializeField] private float _amplitude = 0.2f;

        private Vector3 _originPos;

        private bool _updateOriginPOs;

        private void Start()
        {
            Room.OnRoomChangingStart += () => _updateOriginPOs = true;

            Room.OnRoomChangeFinish += () =>
            {
                _originPos = this.transform.position;
                _updateOriginPOs = false;
            };
        }

        void FixedUpdate()
        {
            if (_updateOriginPOs)
            {
                _originPos = this.transform.position;
            }

            Vector3 direction = _player.position - _originPos;

            Vector3 pos = new Vector3(
                x: _originPos.x + direction.x * _amplitude,
                y: _originPos.y,
                z: _originPos.z + direction.z * _amplitude);

            Vector3 smoothPos = Vector3.Lerp(transform.position, pos, _smoothness);

            this.transform.position = smoothPos;
        }
    }
}
