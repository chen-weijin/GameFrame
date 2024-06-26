using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 动态创建GameObject帮助接口
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class GameObjectHelper
{

    /// <summary>
    /// 创建个RectTransform空对象
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static GameObject CreateEmptyUIObject()
    {
        GameObject gObject = new GameObject("EmptyGameObject", typeof(RectTransform));
        gObject.layer = LayerMask.NameToLayer("UI");

        var rectTransform = gObject.transform as RectTransform;
        rectTransform.anchorMin = layout.center;
        rectTransform.anchorMax = layout.center;
        rectTransform.pivot = layout.center;
        return gObject;
    }
}
