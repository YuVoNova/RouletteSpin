using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RouletteSpin.UI
{
    public class ButtonReviveScript : ButtonBaseScript
    {
        private void Awake()
        {
            _buttonEvent = ButtonEvent.Revive;
        }
    }
}
