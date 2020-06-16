using Assets.Scripts.Common;
using Assets.Scripts.Enemies;
using Assets.Scripts.Pickups;
using Assets.Scripts.ScriptableObjects;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Rooms
{
    public class Room : MonoBehaviour
    {
        public static event Action OnRoomChangingStart;
        public static event Action OnRoomChangeFinish;
        public static event Action<RoomType> OnRoomEnter;

        [SerializeField] private RoomType _roomType;
        [SerializeField] private Transform _respawnPosition;

        [Header("Linked Rooms")]
        [SerializeField] private Room _nextRoom;

        [Header("Current Room Active Doors")]
        [SerializeField] private Door _enterDoor;
        [SerializeField] private Door _exitDoor;

        [Header("View")]
        [SerializeField] private Transform _cameraSnap;

        [Header("Objects")]
        [SerializeField] private AidKit _aidKit;
        [SerializeField] private Transform _enemyContainer;

        private Camera _camera;
        private PlayerSO _playerData;

        private List<GameObject> _activeEnemies;
        private List<GameObject> _initialEnemies;

        public RoomType RoomType => _roomType;

        public Room NextRoom => _nextRoom;
        public Room PrevRoom { get; private set; }

        public Door EnterDoor => _enterDoor;
        public Door ExitDoor => _exitDoor;

        public bool IsActive { get; set; }

        private void Start()
        {
            _camera = Camera.main;

            _playerData = GameObject
                .FindGameObjectWithTag(Tags.Player)
                .GetComponent<Player.Player>().PlayerData;

            if (_enemyContainer != null)
            {
                _activeEnemies = _enemyContainer
                    .GetComponentsInChildren<Enemy>()
                    .Select(x => x.gameObject)
                    .Where(x => x.activeSelf)
                    .ToList();

                _initialEnemies = _enemyContainer
                    .GetComponentsInChildren<Enemy>()
                    .Select(x => Instantiate(x.gameObject, _enemyContainer))
                    .ToList();

                _initialEnemies.ForEach(x =>
                {
                    x.SetActive(false);
                });
            }

            if (this.NextRoom != null)
            {
                this.NextRoom.PrevRoom = this;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                ClosePrevRoom();

                this.IsActive = true;

                if (this.PrevRoom != null)
                {
                    this.PrevRoom.IsActive = false;
                }

                if (this._respawnPosition != null)
                {
                    _playerData.RespawnPosition = _respawnPosition.position;
                }

                OnRoomEnter?.Invoke(this.RoomType);

                _camera.transform
                    .DOMove(_cameraSnap.position, 1.5f)
                    .SetEase(Ease.OutQuint)
                    .OnStart(() =>
                    {
                        OnRoomChangingStart?.Invoke();
                    })
                    .OnComplete(() =>
                    {
                        OnRoomChangeFinish?.Invoke();
                    });
            }
        }

        public void OpenNextRoom()
        {
            this.ExitDoor?.Open();
            this.NextRoom?.EnterDoor.Open();
        }

        public void ClosePrevRoom()
        {
            this.EnterDoor?.Close();
            this.PrevRoom?.ExitDoor.Close();
        }

        #region Save / Load logic

        public void OnLoad(object _)
        {
            if (_enemyContainer != null)
            {
                RegenerateEnemies();
            }

            if (_aidKit != null)
            {
                ActivateAidKit();
            }
        }

        private void RegenerateEnemies()
        {
            _activeEnemies.ForEach(Destroy);

            _activeEnemies = _initialEnemies
                .Select(x => Instantiate(x.gameObject, _enemyContainer))
                .ToList(); 
            
            _activeEnemies.ForEach(x =>
            {
                x.SetActive(true);
            });
        }

        private void ActivateAidKit()
        {
            _aidKit.gameObject.SetActive(true);
        }

        #endregion
    }
}
