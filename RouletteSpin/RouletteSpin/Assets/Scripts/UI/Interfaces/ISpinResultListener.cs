using UnityEngine;
using RouletteSpin.Item;

namespace RouletteSpin.UI
{
    public interface ISpinResultListener
    {
        public void HandleSpinResult(ItemPackage resultItem);
    }
}
