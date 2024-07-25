using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 使用精灵图片的文本，主要用在图片上挂文字防止合批被打断
/// </summary>
[AddComponentMenu("UI/Sprite Text")]
[RequireComponent(typeof(RectTransform))]
public class SpriteText : MonoBehaviour
{
    private const string Sprite_Text_Child_Name = "Sprite_Text_Image";
    private static readonly Vector2 Text_Image_Anchor = Vector2.zero;
    
    [Serializable]
    public class FontSetting
    {
        public char character;
        public Sprite sprite;
    }

    [Tooltip("使用精灵图片的文本，主要用途是防止合批被打断")]
    [SerializeField]
    private string _text = string.Empty;
    public string text
    {
        get
        {
            return _text;
        }
        set
        {
            if (_text != value)
            {
                _text = value;

                _UpdateText();
            }
        }
    }

    [Tooltip("文本之间的间隙")]
    [SerializeField]
    private float _spacing = 0f;
    public float spaceing
    {
        get
        {
            return _spacing;
        }
        set
        {
            if (_spacing != value)
            {
                _spacing = value;
                _ResetPosition();
            }
        }
    }

    [SerializeField]
    private Color _color = Color.white;
    public Color color
    {
        get
        {
            return _color;
        }
        set
        {
            if (_color != value)
            {
                _color = value;

                if (_textImages == null) return;
                foreach (var image in _textImages)
                {
                    image.color = _color;
                }
            }
        }
    }

    [Tooltip("请配置key以及对应纹理图片")]
    [SerializeField]
    private FontSetting[] _fontSettings;
    public FontSetting[] fontSettings
    {
        get
        {
            return _fontSettings;
        }
        set
        {
            _fontSettings = value;
        }
    }

    /// <summary>
    /// 获取当前显示的精灵图片集合
    /// </summary>
    /// <returns></returns>
    public List<Image> GetTexts()
    {
        if (_textImages == null) return new List<Image>();

        return _textImages.GetRange(0, text.Length);
    }

    [HideInInspector]
    [SerializeField]
    private List<Image> _textImages;

    private void _UpdateText()
    {
        if (_textImages == null) return;

        // 隐藏多余image
        _HideTextImages();

        // 确保image足够
        _MakeTextImages();

        _ResetPosition();
    }

    private void _HideTextImages()
    {
        for (int i = text.Length; i < _textImages.Count; i++)
        {
            _textImages[i].gameObject.SetActive(false);
        }
    }

    private void _MakeTextImages()
    {
        if (_fontSettings == null || _fontSettings.Length == 0)
        {
            Debug.LogWarning("错误提示：未配置Font Settings");
            return;
        }

        for (int i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            var fontSetting = _GetFontSetting(ch);
            if (fontSetting == null)
            {
                Debug.LogWarning($"错误提示：字符{ch}不存在，请检查Font Setting是否配置");
                continue;
            }

            var textImage = _GetImage(i);
            textImage.sprite = fontSetting.sprite;
            textImage.SetNativeSize();
            textImage.color = _color;
        }
    }

    // 重复利用，没有就创建
    private Image _GetImage(int index)
    {
        if (_textImages.Count > index)
        {
            return _textImages[index];
        }

        var textImage = _CreateImage();
        _textImages.Add(textImage);
        return textImage;
    }

    private Image _CreateImage()
    {
        GameObject gObject = new GameObject(Sprite_Text_Child_Name, typeof(RectTransform));
        gObject.layer = LayerMask.NameToLayer("UI");
        gObject.hideFlags = HideFlags.HideInHierarchy;

        var imgTrans = gObject.transform as RectTransform;
        imgTrans.anchorMax = Vector2.zero;
        imgTrans.anchorMin = Vector2.zero;
        imgTrans.pivot = Text_Image_Anchor;
        imgTrans.SetParent(transform, false);

        var image = gObject.AddComponent<Image>();
        image.raycastTarget = false;
        return image;
    }

    private FontSetting _GetFontSetting(char ch)
    {
        foreach (var font in _fontSettings)
        {
            if (font.character == ch)
            {
                return font;
            }
        }
        return null;
    }

    private void _ResetPosition()
    {
        if (_textImages.Count==0)
        {
            return;
        }

        var posx = 0.0f;
        var height = 0.0f;
        var count = Math.Min(text.Length, _textImages.Count);
        for (int i = 0; i < count; i++)
        {
            var textImage = _textImages[i];
            textImage.gameObject.SetActive(true);

            var rectTrans = textImage.rectTransform;
            rectTrans.anchoredPosition = cc.p(posx, 0);

            var size = rectTrans.sizeDelta;
            posx += _spacing + size.x;
            height = size.y;
        }
        RectTransform trs = transform as RectTransform;
        trs.sizeDelta = cc.p(posx-_spacing, height);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (gameObject.activeInHierarchy == false)
        {
            return;
        }

        if (_textImages == null) return;

        StartCoroutine(IEDelayApply());
    }

    private IEnumerator IEDelayApply()
    {
        yield return null;

        // 编辑器模式全部删了重新创建，防止有人一下子搞太多
        _ClearTextImages();

        _MakeTextImages();

        _ResetPosition();
    }

    private void _ClearTextImages()
    {
        if (_textImages == null) return;
        
        var children = gameObject.GetChildren();
        for (var i = 0; i < children.Count; i++)
        {
            var child = children[i].gameObject;
            if (PrefabUtility.IsPartOfPrefabInstance(child) == false &&
                child.name == Sprite_Text_Child_Name)
            {
                DestroyImmediate(child);
            }
        }
        _textImages.Clear();
    }
#endif
}
