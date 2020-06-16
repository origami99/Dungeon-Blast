using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponInfo", menuName = "Data/Weapon/WeaponInfo")]
    public class WeaponInfoSO : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private ElementType _element;

        public string Name 
        {
            get => _name;
            set => _name = value;
        }

        public ElementType Element => _element;
    }
}
