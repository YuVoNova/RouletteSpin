using UnityEngine;

namespace RouletteSpin.UI
{
    [CreateAssetMenu(fileName = "New SpinLevelImageData", menuName = "RouletteSpin/UI/SpinLevelImageData")]
    public class SpinLevelImageDataSO : ScriptableObject
    {
        public Sprite BronzeBackgroundSprite;
        public Sprite SilverBackgroundSprite;
        public Sprite GoldBackgroundSprite;

        public Sprite BronzeFrameSprite;
        public Sprite SilverFrameSprite;
        public Sprite GoldFrameSprite;

        public Sprite CurrentBackgroundSprite;
    }
}
