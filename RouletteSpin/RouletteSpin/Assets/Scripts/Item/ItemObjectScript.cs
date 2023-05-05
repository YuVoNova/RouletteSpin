using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RouletteSpin.Item;

namespace RouletteSpin.UI
{
    public class ItemObjectScript : MonoBehaviour
    {
        [SerializeField] private Image ItemImage;
        [SerializeField] private AspectRatioFitter Fitter;
        [SerializeField] private TextMeshProUGUI ItemAmountText;

        private ItemPackage _itemPackage;

        public void SetItem(ItemSO item, int amount)
        {
            ItemImage.sprite = item.ItemIcon;
            Fitter.aspectRatio = item.ItemIcon.rect.width / item.ItemIcon.rect.height;
            
            if (item.Id == "Bomb")
            {
                ItemAmountText.text = "";
            }
            else
            {
                ItemAmountText.text = "x" + amount;
            }

            _itemPackage = new ItemPackage { Item = item, Amount = amount };
        }

        public ItemPackage GetItem()
        {
            return _itemPackage;
        }
    }
}
