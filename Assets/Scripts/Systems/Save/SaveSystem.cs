using Assets.Scripts.Common;
using Assets.Scripts.Pickups;
using Assets.Scripts.Rooms;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Base;
using Assets.Scripts.Utils;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Systems.Save
{
    public class SaveSystem : MonoBehaviour
    {
        public static event Action<DataSnapshot> OnLoad;
        public static event Action<DataSnapshot> OnSave;

        private const string SNAPSHOT_NAME = "snapshot.save";

        [SerializeField] private Player.Player _player;

        public DataSnapshot Snapshot { get; set; }

        private void Start()
        {
            // RegisterSaveSnapshotEvents & RegisterLoadSnapshotEvents define
            // WHEN the snapshot should be SAVED or LOADED
            RegisterSaveSnapshotEvents();
            RegisterLoadSnapshotEvents();

            // ============ \(^-^)/ ============ \\

            // RegisterOnSaveEvents & RegisterOnLoadEvents define
            // WHAT logic should be executed when the snapshot is SAVED or LOADED
            RegisterOnSaveEvents();
            RegisterOnLoadEvents();
        }

        public void SaveSnapshot()
        {
            OnSave?.Invoke(this.Snapshot);

            JsonSystem.SaveJson(this.Snapshot, SNAPSHOT_NAME);
        }

        public void LoadSnapshot()
        {
            if (JsonSystem.GetJson(SNAPSHOT_NAME, out DataSnapshot snap))
            {
                this.Snapshot = snap;

                OnLoad?.Invoke(snap);

                return;
            }

            throw new Exception("Snapshot cannot be loaded.");
        }

        public void SetupInitialSnapshot()
        {
            this.Snapshot = GetInitialSnapshot();

            ResetData();

            JsonSystem.SaveJson(this.Snapshot, SNAPSHOT_NAME);

            LoadSnapshot();
        }

        public bool DoesSnapshotExists()
            => JsonSystem.GetJson(SNAPSHOT_NAME, out DataSnapshot _);

        #region Register Events

        private void RegisterSaveSnapshotEvents()
        {
            Room.OnRoomEnter += roomType =>
            {
                if (roomType == RoomType.Shop)
                {
                    SaveSnapshot();
                }
            };
        }

        private void RegisterLoadSnapshotEvents()
        {
            Player.Player.OnDeath += LoadSnapshot;
        }


        // =========YOU'RE========= \(^-^)/ =========AWESOME========= \\


        private void RegisterOnSaveEvents()
        {
            OnSave += snap =>
            {
                _player.OnSave(snap);

                Resources.FindObjectsOfTypeAll<WeaponShopItem>()
                    .ToList()
                    .ForEach(x => x.OnSave(snap));

                Resources.FindObjectsOfTypeAll<AidKitShopItem>()
                    .ToList()
                    .ForEach(x => x.OnSave(snap));
            };
        }

        private void RegisterOnLoadEvents()
        {
            OnLoad += snap =>
            {
                _player.OnLoad(snap);

                FindObjectsOfType<Room>()
                    .ToList()
                    .ForEach(x => x.OnLoad(snap));

                FindObjectsOfType<Coin>()
                    .ToList()
                    .ForEach(x => x.OnLoad(snap));

                Resources.FindObjectsOfTypeAll<WeaponShopItem>()
                    .ToList()
                    .ForEach(x => x.OnLoad(snap));

                Resources.FindObjectsOfTypeAll<AidKitShopItem>()
                    .ToList()
                    .ForEach(x => x.OnLoad(snap));
            };
        }

        #endregion

        private void ResetData()
        {
            Resources.FindObjectsOfTypeAll<WeaponShopItem>()
                .ToList()
                .ForEach(x => x.ResetItem(this.Snapshot));

            Resources.FindObjectsOfTypeAll<AidKitShopItem>()
                .ToList()
                .ForEach(x => x.ResetItem(this.Snapshot));
        }


        private DataSnapshot GetInitialSnapshot()
        {
            Vector3 initialSpawnPoint = Globals.Player.InitialSpawnPoint;

            var snap = new DataSnapshot(initialSpawnPoint, 100, 0);

            return snap;
        }
    }
}
