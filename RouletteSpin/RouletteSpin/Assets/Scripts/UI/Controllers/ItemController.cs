using RouletteSpin.Game;
using RouletteSpin.Item;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RouletteSpin.UI
{
    public class ItemController : MonoBehaviour, ISpinCompletedListener, INextSpinListener, IResetListener
    {
        public static ItemController Instance;

        public event Action<ItemPackage> SpinResultEvent;

        [SerializeField] private ItemDataSO ItemData;

        [SerializeField] private ItemSO BombItem;

        [SerializeField] private RouletteSpinnerScript RouletteSpinner;

        [SerializeField] private ItemObjectScript[] ItemObjects;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            RouletteSpinner.SpinCompletedEvent += HandleSpinCompleted;
            GameController.Instance.NextSpinEvent += HandleNextSpin;
            GameController.Instance.ResetEvent += HandleReset;

            SetItems();
        }

        private void OnDestroy()
        {
            RouletteSpinner.SpinCompletedEvent -= HandleSpinCompleted;
            GameController.Instance.NextSpinEvent -= HandleNextSpin;
            GameController.Instance.ResetEvent -= HandleReset;
        }

        public void HandleSpinCompleted(float degree)
        {
            int modDegree = (int)degree % 360;
            int selectedItemIndex = modDegree / (360 / ItemObjects.Length);

            SpinResultEvent?.Invoke(ItemObjects[selectedItemIndex].GetItem());
        }

        public void HandleNextSpin()
        {
            SetItems();
        }

        public void HandleReset()
        {
            SetItems();
        }

        private void SetItems()
        {
            int spinCount = GameController.Instance.GetCurrentSpin();

            List<int> indexList = new List<int>();

            for (int i = 0; i < ItemObjects.Length; i++)
            {
                indexList.Add(i);
            }

            for (int i = 0; i < ItemObjects.Length; i++)
            {
                int randomIndex = indexList[UnityEngine.Random.Range(0, indexList.Count)];
                indexList.Remove(randomIndex);

                ItemSO randomItem;

                if (i == 0 && spinCount % 5 != 0)
                {
                    randomItem = BombItem;
                }
                else
                {
                    randomItem = ItemData.Items[UnityEngine.Random.Range(0, ItemData.Items.Count)];
                }

                int amount = UnityEngine.Random.Range(randomItem.MinAmount, randomItem.MaxAmount + 1) * randomItem.Multiplier * spinCount;

                if (spinCount == 30)
                {
                    amount *= 10;
                }
                else if (spinCount % 5 == 0)
                {
                    amount *= 5;
                }

                ItemObjects[randomIndex].SetItem(randomItem, amount);
            }
        }
    }
}
