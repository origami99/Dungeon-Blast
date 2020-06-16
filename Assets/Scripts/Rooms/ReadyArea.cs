using System;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Rooms.RoomGoals
{
    public class ReadyArea : MonoBehaviour
    {
        public static event Action<Room> OnAreaEnter;

        [SerializeField] private Room _room;

        private bool _wasTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!_wasTriggered && other.CompareTag(Tags.Player))
            {
                //_wasTriggered = true;
                OnAreaEnter?.Invoke(_room);
            }
        }
    }
}
