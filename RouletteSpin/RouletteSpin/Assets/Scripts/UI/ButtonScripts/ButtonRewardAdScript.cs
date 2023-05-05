using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RouletteSpin.UI
{
    public class ButtonRewardAdScript : ButtonBaseScript
    {
        private void Awake()
        {
            _buttonEvent = ButtonEvent.RewardAd;
        }
    }
}
