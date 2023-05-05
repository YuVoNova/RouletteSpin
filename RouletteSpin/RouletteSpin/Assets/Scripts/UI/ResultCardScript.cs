using UnityEngine;
using UnityEngine.UI;
using RouletteSpin.Item;
using TMPro;
using DG.Tweening;

namespace RouletteSpin.UI
{
    public class ResultCardScript : MonoBehaviour, ISpinResultListener
    {
        [SerializeField] private Image CardBackgroundImage;
        [SerializeField] private Image ItemImage;
        [SerializeField] private AspectRatioFitter Fitter;
        [SerializeField] private TextMeshProUGUI ItemAmountText;

        [SerializeField] private Sprite NormalCardBackground;
        [SerializeField] private Sprite BombCardBackground;

        private void Start()
        {
            ItemController.Instance.SpinResultEvent += HandleSpinResult;
        }

        private void OnDestroy()
        {
            ItemController.Instance.SpinResultEvent -= HandleSpinResult;
        }

        public void HandleSpinResult(ItemPackage resultItemPackage)
        {
            ItemImage.sprite = resultItemPackage.Item.ItemIcon;
            Fitter.aspectRatio = resultItemPackage.Item.ItemIcon.rect.width / resultItemPackage.Item.ItemIcon.rect.height;

            if (resultItemPackage.Item.Id == "Bomb")
            {
                CardBackgroundImage.sprite = BombCardBackground;
                ItemAmountText.text = "";
            }
            else
            {
                CardBackgroundImage.sprite = NormalCardBackground;
                ItemAmountText.text = "x" + resultItemPackage.Amount;
            }

            transform.DOPunchScale(Vector3.one, 2f, 0, 0);
        }


    }
}
