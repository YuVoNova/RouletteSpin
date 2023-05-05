using System;
using UnityEngine;

namespace RouletteSpin.Item
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Item", menuName = "RouletteSpin/Item/Item")]
    public class ItemSO : ScriptableObject
    {
        public string Id;

        public Sprite ItemIcon;

        public int MinAmount;
        public int MaxAmount;

        public int Multiplier;
    }
}
