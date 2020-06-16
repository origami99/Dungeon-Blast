using System;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public Weapon Weapon { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            bool isEnvironmentHitted = other.gameObject.layer == Layers.Environment;
            bool isEnemyHitted = other.CompareTag(Tags.Enemy);

            bool isPlayerShooting = false;
            if (this.Weapon != null)
            {
                isPlayerShooting = this.Weapon.Holder.tag == Tags.Player;
            }

            if (isEnvironmentHitted || (isPlayerShooting && isEnemyHitted))
            {
                Destroy(this.gameObject);
            }
        }
    }
}