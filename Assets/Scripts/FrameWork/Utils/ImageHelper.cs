
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 动态创建Image帮助接口
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class ImageHelper
{
    public enum FixedSceenType
    {
        Auto,       // 等比缩放且保证不会看到空白，默认使用该模式
        Height,     // 按等高缩放
        Width,      // 按等宽缩放
        Both,       // 按等高/等宽填满屏幕，会有压缩
    }

    /// <summary>
    /// 创建带有Image组件的空Object
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static GameObject CreateEmptyImage(string name = "Image")
    {
        GameObject gObject = new GameObject(name, typeof(RectTransform));
        gObject.layer = LayerMask.NameToLayer("UI");

        var half = layout.center;
        var rectTransform = gObject.transform as RectTransform;
        rectTransform.anchorMin = half;
        rectTransform.anchorMax = half;
        rectTransform.pivot = half;

        var image = gObject.AddComponent<Image>();
        image.raycastTarget = false;

        return gObject;
    }

    /// <summary>
    /// 创建带有图片的Image，请确保ab包已提前加载
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="assetKey"></param>
    /// <returns></returns>
    //[UnityEngine.Scripting.Preserve]
    //public static GameObject CreateImage(string assetName, string key)
    //{
    //    GameObject gObject = new GameObject("Image", typeof(RectTransform));
    //    gObject.layer = LayerMask.NameToLayer("UI");

    //    var rectTransform = gObject.transform as RectTransform;
    //    rectTransform.anchorMin = layout.center;
    //    rectTransform.anchorMax = layout.center;
    //    rectTransform.pivot = layout.center;

    //    var image = gObject.AddComponent<Image>();
    //    image.raycastTarget = false;
    //    image.sprite = AssetsManager.Instance.Load<Sprite>(Path.Combine(assetName, key));
    //    image.SetNativeSize();

    //    return gObject;
    //}

    /// <summary>
    /// 创建ui纹理贴图
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Texture2D CreateTexture2D(byte[] datas)
    {
        var format = SystemInfo.SupportsTextureFormat(TextureFormat.ASTC_5x5) ? TextureFormat.ASTC_5x5 : TextureFormat.RGBA32;
        var texture = new Texture2D(10, 10, format, false, false);
        texture.LoadImage(datas);
        return texture;
    }

    /// <summary>
    /// 通过数据创建纹理图像
    /// </summary>
    /// <param name="datas"> 纹理图片数据 </param>
    /// <param name="pivot"> 锚点，如layout.center </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Sprite CreateSprite(byte[] datas, Vector2 pivot)
    {
        var texture = CreateTexture2D(datas);
        var rect = cc.rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rect, pivot);
    }

    /// <summary>
    /// 将背景图拉伸跟屏幕一样大
    /// </summary>
    /// <param name="imgTrans"> 图片的Object对象 </param>
    /// <param name="t"> 拉伸类型，参考FixedSceenType </param>
    [UnityEngine.Scripting.Preserve]
    public static void FixedScreen(RectTransform imgTrans, FixedSceenType t = FixedSceenType.Auto)
    {
        var size = imgTrans.sizeDelta;
        float scaleX = display.width / size.x;
        float scaleY = display.height / size.y;

        if (FixedSceenType.Auto == t)
        {
            var scale = Mathf.Max(scaleX, scaleY);
            imgTrans.localScale = cc.p3(scale, scale, 1);
        }
        else if (FixedSceenType.Height == t)
        {
            imgTrans.localScale = cc.p3(scaleY, scaleY, 1);
        }
        else if (FixedSceenType.Width == t)
        {
            imgTrans.localScale = cc.p3(scaleX, scaleX, 1);
        }
        else
        {
            imgTrans.localScale = cc.p3(scaleX, scaleY, 1);
        }
    }
}
