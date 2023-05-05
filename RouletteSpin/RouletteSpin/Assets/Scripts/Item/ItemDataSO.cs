using System;
using System.Collections.Generic;
using UnityEngine;

namespace RouletteSpin.Item
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "RouletteSpin/Item/ItemData")]
    public class ItemDataSO : ScriptableObject
    {
        public List<ItemSO> Items;
    }
}
