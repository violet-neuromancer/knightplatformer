using Settings;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class InventoryItem
    {
        [SerializeField] private CrystalType crystalType;
        [SerializeField] private float quantity;

        public CrystalType CrystalType
        {
            get => crystalType;
            set => crystalType = value;
        }

        public float Quantity
        {
            get => quantity;
            set => quantity = value;
        }
    }
}
