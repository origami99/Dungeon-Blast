using Assets.Scripts.Shop;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject _shopMenu;

        private void Start()
        {
            ShopCarpet.OnShopEnter += Open;
            ShopCarpet.OnShopExit += Close;
        }

        public void Open() => _shopMenu.SetActive(true);

        public void Close() => _shopMenu.SetActive(false);
    }
}
