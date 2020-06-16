using UnityEngine;

namespace Assets.Scripts.Rooms.RoomGoals
{
    [RequireComponent(typeof(Room))]
    public class TriggerRoomGoal : MonoBehaviour
    {
        private Room _room;

        private void Start()
        {
            ReadyArea.OnAreaEnter += GoalComplete;
        
            _room = this.GetComponent<Room>();

            _room.ExitDoor.EnableDoorTrigger();
        }

        private void OnDestroy()
        {
            ReadyArea.OnAreaEnter -= GoalComplete;
        }

        private void GoalComplete(Room room)
        {
            room.OpenNextRoom();
        }
    }
}
