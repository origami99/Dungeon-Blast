using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Behaviours
{
    public abstract class PickupBehaviour : MonoBehaviour
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Player))
            {
                Collect();
            }
        }

        public abstract void Collect();
    }
}
