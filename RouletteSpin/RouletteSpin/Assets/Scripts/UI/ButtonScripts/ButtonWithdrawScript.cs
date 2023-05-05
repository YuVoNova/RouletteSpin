using RouletteSpin.Game;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace RouletteSpin.UI
{
    public class ButtonWithdrawScript : ButtonBaseScript, INextSpinListener, IResetListener
    {
        private float _inMove;
        private float _outMove;

        private void Awake()
        {
            _buttonEvent = ButtonEvent.Withdraw;

            _inMove = GetComponent<RectTransform>().offsetMin.x;
            _outMove = _inMove - GetComponent<RectTransform>().rect.width * 2f;
        }

        private void Start()
        {
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
            HideButton();
        }

        public void HandleNextSpin()
        {
            ShowButton();
        }

        public void HandleReset()
        {
            ShowButton();
        }

        private void HideButton()
        {
            GetComponent<Button>().interactable = false;

            GetComponent<RectTransform>().DOAnchorPosX(_outMove, 0.5f, true);
        }

        private void ShowButton()
        {
            GetComponent<RectTransform>().DOAnchorPosX(_inMove, 0.5f, true);

            GetComponent<Button>().interactable = true;
        }
    }
}
