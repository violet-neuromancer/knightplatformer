using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUIButton : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text label;
        [SerializeField] private Text count;
        [SerializeField] private List<Sprite> sprites;

        private InventoryItem itemData;

        public InventoryItem ItemData
        {
            get => itemData;
            set => itemData = value;
        }

        public InventoryUsedCallback Callback { get; set; }

        private void Start()
        {
            var spriteNameToSearch = itemData.CrystalType.ToString().ToLower();
            image.sprite = sprites.Find(x => x.name.Contains(spriteNameToSearch));
            label.text = spriteNameToSearch;
            count.text = itemData.Quantity.ToString();
            gameObject.GetComponent<Button>().onClick.AddListener(() => Callback(this));
        }
    }
}