using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// PointerEventData扩展
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class PointerEventDataExtension
{
    /// <summary>
    /// 适配后，正确的UI点击位置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 LocationPosition(this PointerEventData data)
    {
        return data.position / display.srceenScaleFactor;
    }

    /// <summary>
    /// 前一次点击位置
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 PreLocationPosition(this PointerEventData data)
    {
        return (data.position - data.delta) / display.srceenScaleFactor;
    }
}
