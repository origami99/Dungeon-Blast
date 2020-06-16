using Assets.Scripts.Enemies;
using Assets.Scripts.Utils.Extenstions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Rooms.RoomGoals
{
    [RequireComponent(typeof(Room))]
    public class CombatRoomGoal : MonoBehaviour
    {
        private Room _room;

        private void Start()
        {
            Enemy.OnDeath += _ => GoalCompletedCheck();

            _room = this.GetComponent<Room>();

            GoalCompletedCheck();
        }

        public void GoalCompletedCheck()
        {
            if (_room.IsActive)
            {
                List<Enemy> remainingEnemies = gameObject.transform
                    .FindObjectsWithTag(Tags.Enemy)
                    .Select(x => x.GetComponent<Enemy>())
                    .Where(x => x.gameObject.activeSelf)
                    .Where(x => !x.IsDead())
                    .ToList();

                if (!remainingEnemies.Any())
                {
                    _room.OpenNextRoom();
                }
            }
        }
    }
}
