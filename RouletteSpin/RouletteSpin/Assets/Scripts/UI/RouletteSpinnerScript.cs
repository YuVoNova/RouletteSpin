using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RouletteSpin.Game;

namespace RouletteSpin.UI
{
    public class RouletteSpinnerScript : MonoBehaviour, INextSpinListener, IResetListener
    {
        public event Action<float> SpinCompletedEvent;

        [SerializeField] private Image RouletteImage;
        [SerializeField] private Image IndicatorImage;

        [SerializeField] private Sprite BronzeRouletteSprite;
        [SerializeField] private Sprite SilverRouletteSprite;
        [SerializeField] private Sprite GoldRouletteSprite;

        [SerializeField] private Sprite BronzeIndicatorSprite;
        [SerializeField] private Sprite SilverIndicatorSprite;
        [SerializeField] private Sprite GoldIndicatorSprite;

        private float _targetDegree;

        private void Start()
        {
            PrepareRoulette();

            MenuController.Instance.SpinButtonEvent += HandleSpin;
            GameController.Instance.NextSpinEvent += HandleNextSpin;
            GameController.Instance.ResetEvent += HandleReset;
        }

        private void OnDestroy()
        {
            MenuController.Instance.SpinButtonEvent -= HandleSpin;
            GameController.Instance.NextSpinEvent -= HandleNextSpin;
            GameController.Instance.ResetEvent -= HandleReset;
        }

        private void HandleSpin()
        {
            _targetDegree = UnityEngine.Random.Range(0, 8) * 45f + 360f * 3;

            transform.DORotate(new Vector3(0f, 0f, -_targetDegree), 3f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(() => OnSpinComplete());
        }

        public void HandleNextSpin()
        {
            PrepareRoulette();
        }

        public void HandleReset()
        {
            PrepareRoulette();
        }

        private void OnSpinComplete()
        {
            transform.DOPunchScale(new Vector3(0.25f, 0.25f, 0.25f), 0.25f, 1, 0.5f);
            SpinCompletedEvent?.Invoke(_targetDegree);
        }

        private void PrepareRoulette()
        {
            int currentSpin = GameController.Instance.GetCurrentSpin();

            transform.localEulerAngles = Vector3.zero;

            // Gold
            if (currentSpin == 30)
            {
                SetRouletteImages(GoldRouletteSprite, GoldIndicatorSprite);
            }
            else
            {
                // Silver
                if (currentSpin % 5 == 0)
                {
                    SetRouletteImages(SilverRouletteSprite, SilverIndicatorSprite);
                }
                // Bronze
                else
                {
                    SetRouletteImages(BronzeRouletteSprite, BronzeIndicatorSprite);
                }
            }
        }

        private void SetRouletteImages(Sprite rouletteSprite, Sprite indicatorSprite)
        {
            RouletteImage.sprite = rouletteSprite;
            IndicatorImage.sprite = indicatorSprite;
        }
    }
}
