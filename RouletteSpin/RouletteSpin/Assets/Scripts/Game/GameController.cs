using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RouletteSpin.UI;
using RouletteSpin.Item;
using System;

namespace RouletteSpin.Game
{
    public class GameController : MonoBehaviour, ISpinResultListener
    {
        public static GameController Instance;

        public event Action FailedEvent;
        public event Action NextSpinEvent;
        public event Action ResetEvent;

        public InventorySO Inventory;

        private List<ItemPackage> _wonItemsList;

        private int _currentSpin;

        private void Awake()
        {
            Instance = this;

            Inventory.Initialize();

            _wonItemsList = new List<ItemPackage>();

            _currentSpin = 1;
        }

        private void Start()
        {
            ItemController.Instance.SpinResultEvent += HandleSpinResult;
            MenuController.Instance.WithdrawButtonEvent += HandleWithdrawButton;
            MenuController.Instance.ReviveButtonEvent += HandleReviveButton;
            MenuController.Instance.GiveUpButtonEvent += HandleGiveUpButton;
            MenuController.Instance.RewardAdButtonEvent += HandleRewardAdButton;
        }

        private void OnDestroy()
        {
            ItemController.Instance.SpinResultEvent -= HandleSpinResult;
            MenuController.Instance.WithdrawButtonEvent -= HandleWithdrawButton;
            MenuController.Instance.ReviveButtonEvent -= HandleReviveButton;
            MenuController.Instance.GiveUpButtonEvent -= HandleGiveUpButton;
            MenuController.Instance.RewardAdButtonEvent -= HandleRewardAdButton;
        }

        public void HandleSpinResult(ItemPackage resultItem)
        {
            if (resultItem.Item.Id == "Bomb")
            {
                Invoke("Failed", 2f);
            }
            else
            {
                _wonItemsList.Add(resultItem);

                Invoke("NextSpin", 2f);
            }
        }

        private void HandleWithdrawButton()
        {
            SaveItemsToInventory();

            Reset();
        }

        private void HandleReviveButton()
        {
            Invoke("NextSpin", 0.5f);
        }

        private void HandleGiveUpButton()
        {
            Reset();
        }

        private void HandleRewardAdButton()
        {
            Invoke("NextSpin", 0.5f);
        }

        private void Failed()
        {
            FailedEvent?.Invoke();
        }

        private void NextSpin()
        {
            if (_currentSpin == 30)
            {
                SaveItemsToInventory();

                Reset();
            }
            else
            {
                _currentSpin = Mathf.Clamp(_currentSpin + 1, 1, 30);

                NextSpinEvent.Invoke();
            }
        }

        private void Reset()
        {
            _wonItemsList.Clear();

            _currentSpin = 1;
            ResetEvent?.Invoke();
        }

        private void SaveItemsToInventory()
        {
            foreach (var itemPackage in _wonItemsList)
            {
                Inventory.AddItem(itemPackage);
            }
        }

        public int GetCurrentSpin()
        {
            return _currentSpin;
        }
    }
}
