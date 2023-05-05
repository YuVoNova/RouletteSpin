using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RouletteSpin.UI
{
    public class ButtonGiveUpScript : ButtonBaseScript
    {
        private void Awake()
        {
            _buttonEvent = ButtonEvent.GiveUp;
        }
    }
}
