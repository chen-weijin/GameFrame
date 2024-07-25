
using System;
using UnityEngine;
using UnityEngine.UI;

namespace NGame
{
    //[AddComponentMenu("UI/Sprite State")]
    [RequireComponent(typeof(RectTransform))]
    public class SpriteStatus : MonoBehaviour
    {
        [Serializable]
        public class ImageSetting
        {
            public string name;
            public Sprite sprite;
        }

        [Tooltip("请配置key以及对应纹理图片(温馨提示:功能过测后请误随意改动Key值,如需改动请配合代码调整)")]
        [SerializeField]
        private ImageSetting[] _imageSettings;

        public void Start()
        {
        }

        public void ChangeState(string stateName, bool isResetSize = false)
        {
            if (_imageSettings.Length == 0) return;

            var image = gameObject.EnsureComponent<Image>();

            Sprite sprite = null;
            foreach (var img in _imageSettings)
            {
                if (img.name == stateName)
                {
                    sprite = img.sprite;
                    break;
                }
            }
            if (sprite == null) return;

            image.sprite = sprite;
            if (isResetSize)
            {
                image.SetNativeSize();
            }
        }
    }
}
