using RouletteSpin.Game;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RouletteSpin.UI
{
    public class MenuController : MonoBehaviour, IFailedListener, INextSpinListener
    {
        public static MenuController Instance;

        public event Action SpinButtonEvent;
        public event Action WithdrawButtonEvent;
        public event Action ReviveButtonEvent;
        public event Action GiveUpButtonEvent;
        public event Action RewardAdButtonEvent;

        [SerializeField] private GameObject SafeZoneObject;
        [SerializeField] private RectTransform FailedMenuTransform;

        private List<ButtonBaseScript> _buttons;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
#if PLATFORM_ANDROID
            FindButtons();
#endif

            SubscribeButtonEvents(true);

            GameController.Instance.FailedEvent += HandleFailed;
            GameController.Instance.NextSpinEvent += HandleNextSpin;
        }

        private void OnDestroy()
        {
            SubscribeButtonEvents(false);

            GameController.Instance.FailedEvent -= HandleFailed;
            GameController.Instance.NextSpinEvent -= HandleNextSpin;
        }

        private void OnValidate()
        {
            FindButtons();
        }

        private void FindButtons()
        {
            _buttons = new List<ButtonBaseScript>();
            var buttons = FindObjectsOfType<ButtonBaseScript>();

            foreach (var button in buttons)
            {
                if (button != null)
                {
                    _buttons.Add(button);
                }
            }
        }

        private void SubscribeButtonEvents(bool isTrue)
        {
            foreach (var button in _buttons)
            {
                if (isTrue)
                {
                    button.ButtonClickedEvent += OnButtonClicked;
                }
                else
                {
                    button.ButtonClickedEvent -= OnButtonClicked;
                }
            }
        }

        public void HandleFailed()
        {
            FailedMenuTransform.DOScale(Vector3.one, 0.4f);
        }

        public void HandleNextSpin()
        {
            if (GameController.Instance.GetCurrentSpin() % 5 == 0)
            {
                SafeZoneObject.SetActive(true);
            }
            else
            {
                if (SafeZoneObject.activeSelf)
                {
                    SafeZoneObject.SetActive(false);
                }
            }
        }

        private void OnButtonClicked(ButtonEvent buttonEvent)
        {
            switch (buttonEvent)
            {
                case ButtonEvent.Spin:

                    SpinButtonEvent?.Invoke();

                    break;
                case ButtonEvent.Withdraw:

                    WithdrawButtonEvent?.Invoke();

                    break;
                case ButtonEvent.Revive:

                    FailedMenuTransform.DOScale(Vector3.zero, 0.4f);
                    ReviveButtonEvent?.Invoke();

                    break;
                case ButtonEvent.GiveUp:

                    FailedMenuTransform.DOScale(Vector3.zero, 0.4f);
                    GiveUpButtonEvent?.Invoke();

                    break;
                case ButtonEvent.RewardAd:

                    FailedMenuTransform.DOScale(Vector3.zero, 0.4f);
                    RewardAdButtonEvent?.Invoke();

                    break;
                default:

                    Debug.LogError("ERROR: An undefined button event was called.");

                    break;
            }
        }
    }
}
