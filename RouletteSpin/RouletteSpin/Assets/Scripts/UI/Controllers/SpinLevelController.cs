using RouletteSpin.Game;
using UnityEngine;
using DG.Tweening;

namespace RouletteSpin.UI
{
    public class SpinLevelController : MonoBehaviour, INextSpinListener, IResetListener
    {
        private void Start()
        {
            GameController.Instance.NextSpinEvent += HandleNextSpin;
            GameController.Instance.ResetEvent += HandleReset;
        }

        private void OnDestroy()
        {
            GameController.Instance.NextSpinEvent -= HandleNextSpin;
            GameController.Instance.ResetEvent -= HandleReset;
        }

        public void HandleNextSpin()
        {
            GetComponent<RectTransform>().DOAnchorPosX((GameController.Instance.GetCurrentSpin() - 1) * -50f, 0.25f, true);
        }

        public void HandleReset()
        {
            GetComponent<RectTransform>().DOAnchorPosX(0f, 0.25f, true);
        }
    }
}
