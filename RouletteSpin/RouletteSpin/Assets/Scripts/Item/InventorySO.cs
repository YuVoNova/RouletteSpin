using System;
using System.Collections.Generic;
using UnityEngine;

namespace RouletteSpin.Item
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "RouletteSpin/Item/Inventory")]
    public class InventorySO : ScriptableObject
    {
        public ItemDataSO ItemData;

        public Dictionary<ItemSO, int> ItemDictionary;

        public void Initialize()
        {
            if (ItemDictionary == null)
            {
                ItemDictionary = new Dictionary<ItemSO, int>();

                foreach (var item in ItemData.Items)
                {
                    ItemDictionary.Add(item, 0);
                }
            }
        }

        public void AddItem(ItemPackage itemPackage)
        {
            ItemDictionary[itemPackage.Item] += itemPackage.Amount;
        }
    }

    [Serializable]
    public struct ItemPackage
    {
        public ItemSO Item;
        public int Amount;
    }
}
