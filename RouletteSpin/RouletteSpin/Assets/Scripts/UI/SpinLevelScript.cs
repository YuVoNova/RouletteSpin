using RouletteSpin.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RouletteSpin.UI
{
    public class SpinLevelScript : MonoBehaviour, INextSpinListener, IResetListener
    {
        [SerializeField] private Image SpinLevelBackgroundImage;
        [SerializeField] private Image SpinLevelFrameImage;
        [SerializeField] private TextMeshProUGUI SpinLevelText;

        [SerializeField] private SpinLevelImageDataSO SpinLevelImageData;

        private int _spinLevel;

        private void Start()
        {
            GameController.Instance.NextSpinEvent += HandleNextSpin;
            GameController.Instance.ResetEvent += HandleReset;

#if PLATFORM_ANDROID
            SetSpinLevel();
#endif

            CheckCurrentSpin();
        }

        private void OnDestroy()
        {
            GameController.Instance.NextSpinEvent -= HandleNextSpin;
            GameController.Instance.ResetEvent -= HandleReset;
        }

        private void OnValidate()
        {
            SetSpinLevel();
        }

        private void SetSpinLevel()
        {
            _spinLevel = transform.GetSiblingIndex() + 1;

            SpinLevelText.text = _spinLevel.ToString();
            transform.name = "UI_Parent_SpinLevel_" + _spinLevel;

            SetSpinLevelImages();
        }

        private void SetSpinLevelImages()
        {
            // Gold
            if (_spinLevel == 30)
            {
                SpinLevelBackgroundImage.sprite = SpinLevelImageData.GoldBackgroundSprite;
                SpinLevelFrameImage.sprite = SpinLevelImageData.GoldFrameSprite;
            }
            else
            {
                // Silver
                if (_spinLevel % 5 == 0)
                {
                    SpinLevelBackgroundImage.sprite = SpinLevelImageData.SilverBackgroundSprite;
                    SpinLevelFrameImage.sprite = SpinLevelImageData.SilverFrameSprite;
                }
                // Bronze
                else
                {
                    SpinLevelBackgroundImage.sprite = SpinLevelImageData.BronzeBackgroundSprite;
                    SpinLevelFrameImage.sprite = SpinLevelImageData.BronzeFrameSprite;
                }
            }
        }

        private void CheckCurrentSpin()
        {
            if (_spinLevel == GameController.Instance.GetCurrentSpin())
            {
                SpinLevelBackgroundImage.sprite = SpinLevelImageData.CurrentBackgroundSprite;
            }
        }

        public void HandleNextSpin()
        {
            CheckCurrentSpin();
        }

        public void HandleReset()
        {
            SetSpinLevelImages();

            CheckCurrentSpin();
        }
    }
}
