using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Globals
    {
        public class Player
        {
            public static Vector3 InitialSpawnPoint = new Vector3(0, 1, -2);
        }

        public class Settings
        {
            public const int ROOM_SIZE = 15;
        }

        public class Combat
        {
            public const int MELEE_DAMAGE = 1;
            public const int DAMAGE_MULTIPLAYER = 10;
        }

        public class Tags
        {
            public const string Player = "Player";
            public const string Enemy = "Enemy";
            public const string Weapon = "Weapon";
            public const string Bullet = "Bullet";
            public const string Melee = "Melee";
        }

        public class Layers
        {
            public static int Environment = LayerMask.NameToLayer("Environment");
        }

        public class AnimatorParameters
        {
            public const string MeleeAttack = "MeleeAttack";
        }
    }
}
