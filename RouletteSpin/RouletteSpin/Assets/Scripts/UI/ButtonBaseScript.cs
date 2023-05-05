using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RouletteSpin.UI
{
    public class ButtonBaseScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public event Action<ButtonEvent> ButtonClickedEvent;

        protected ButtonEvent _buttonEvent;

        protected AudioSource _audioSource;

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ButtonClickedEvent?.Invoke(_buttonEvent);
        }
    }

    public enum ButtonEvent
    {
        Spin,
        Withdraw,
        Revive,
        GiveUp,
        RewardAd
    }
}
