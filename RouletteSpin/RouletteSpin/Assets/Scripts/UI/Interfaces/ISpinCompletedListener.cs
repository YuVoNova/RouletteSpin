using UnityEngine;

namespace RouletteSpin.UI
{
    public interface ISpinCompletedListener
    {
        public void HandleSpinCompleted(float degree);
    }
}
