using System;
using UnityEngine;
using static Assets.Scripts.Utils.Globals;

namespace Assets.Scripts.Shop
{
    public class ShopCarpet : MonoBehaviour
    {
        public static event Action OnShopEnter;
        public static event Action OnShopExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                OnShopEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                OnShopExit?.Invoke();
            }
        }
    }
}