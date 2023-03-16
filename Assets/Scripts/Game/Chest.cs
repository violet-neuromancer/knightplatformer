using Settings;
using UnityEngine;

namespace Game
{
    public class Chest : MonoBehaviour
    {
        public InventoryItem itemData = new InventoryItem();

        private void OnTriggerEnter2D(Collider2D chestCollider)
        {
        
            var knight = chestCollider.gameObject.GetComponent<Knight>();
            if (knight != null) Destroy(gameObject);

            if (itemData.CrystalType == CrystalType.Random) itemData.CrystalType = (CrystalType)Random.Range(1, 4);

            if (itemData.Quantity == 0) itemData.Quantity = Random.Range(1, 6);

            GameController.Instance.AddNewInventoryItem(itemData);
        }
    }
}