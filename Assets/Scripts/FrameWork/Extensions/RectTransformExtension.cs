using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 扩展RectTransform
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class RectTransformExtension
{
    /// <summary>
    /// 添加到父节点
    /// </summary>
    /// <param name="t"></param>
    /// <param name="to"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform AddTo(this RectTransform t, RectTransform to)
    {
        t.SetParent(to, false);
        return t;
    }

    /// <summary>
    /// 添加到父节点
    /// </summary>
    /// <param name="t"></param>
    /// <param name="to"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform AddTo(this RectTransform t, GameObject to)
    {
        t.SetParent(to.transform, false);
        return t;
    }

    /// <summary>
    /// 按unity的ui适配规则
    /// </summary>
    /// <param name="t"></param>
    /// <param name="layout"> 相对父节点的锚点 </param>
    /// <param name="offset"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform Layout(this RectTransform t, Vector2 layout, Vector2 offset)
    {
        t.anchorMin = layout;
        t.anchorMax = layout;
        t.anchoredPosition = offset;
        return t;
    }

    /// <summary>
    /// 按unity的ui适配规则
    /// </summary>
    /// <param name="t"></param>
    /// <param name="layout"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform Layout(this RectTransform t, Vector2 layout)
    {
        Layout(t, layout, cc.Vector2Zero);
        return t;
    }

    /// <summary>
    /// 按unity的ui适配规则，如果有刘海屏会额外加上/减去刘海屏的偏移量
    /// </summary>
    /// <param name="t"></param>
    /// <param name="layout"></param>
    /// <param name="offset"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform LayoutScreen(this RectTransform t, Vector2 layout, Vector2 offset)
    {
        var screenOffset = DesignResolution.GetScreenOffset(layout);
        Layout(t, layout, screenOffset + offset);
        return t;
    }

    /// <summary>
    /// 按unity的ui适配规则，如果有刘海屏会额外加上/减去刘海屏的偏移量
    /// </summary>
    /// <param name="t"></param>
    /// <param name="layout"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform LayoutScreen(this RectTransform t, Vector2 layout)
    {
        LayoutScreen(t, layout, Vector2.zero);
        return t;
    }

    /// <summary>
    /// 按原来的锚点设置位置
    /// </summary>
    /// <param name="t"></param>
    /// <param name="position"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetPosition(this RectTransform t, Vector2 position)
    {
        t.anchoredPosition = position;
        return t;
    }

    /// <summary>
    /// local坐标
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetPosition(this RectTransform t)
    {
        return t.anchoredPosition;
    }

    /// <summary>
    /// 获取当前位置在设计分辨率屏幕位置，该接口会动态查找UI相机，请勿在热更函数调用
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetScreenPosition(this RectTransform t)
    {
        var cameraObj = GameObject.Find("UICamera");
        if (cameraObj == null)
        {
            Debug.LogWarning($"GetDesignPosition 未找到UI相机对象");
            return Vector2.zero;
        }

        var uiCamera = cameraObj.GetComponent<Camera>();
        if (uiCamera == null)
        {
            Debug.LogWarning($"GetDesignPosition 未找到UI相机组件");
            return Vector2.zero;
        }

        // 屏幕坐标除以缩放比值才是设计分辨率屏幕坐标
        return uiCamera.WorldToScreenPoint(t.position) / display.srceenScaleFactor;
    }

    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetScreenPosition(this RectTransform t, Camera uiCamera)
    {
        return uiCamera.WorldToScreenPoint(t.position) / display.srceenScaleFactor;
    }

    /// <summary>
    /// 获取当前位置在设计分辨率屏幕Rect位置，该接口会动态查找UI相机，请勿在热更函数调用
    /// 注意：如果父节点有缩放/旋转那位置可能是错的
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Rect GetScreenRect(this RectTransform t)
    {
        var pos = t.GetScreenPosition();
        var size = t.sizeDelta * t.localScale;
        var pos2 = pos - size * t.pivot;
        return cc.rect(pos2, size);
    }

    [UnityEngine.Scripting.Preserve]
    public static Rect GetScreenRect(this RectTransform t, Camera uiCamera)
    {
        var pos = t.GetScreenPosition(uiCamera);
        var size = t.sizeDelta * t.localScale;
        var pos2 = pos - size * t.pivot;
        return cc.rect(pos2, size);
    }

    /// <summary>
    /// 本地坐标转为UI屏幕坐标
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    //public static Vector2 Convert2UIPoint(this RectTransform t)
    //{
    //    return t.position / display.srceenScaleFactor;
    //}

    /// <summary>
    /// 对应Convert2UIPoint，将UI屏幕坐标转为local坐标
    /// </summary>
    /// <param name="t"></param>
    /// <param name="screenPos"></param>
    /// <returns></returns>
    //public static Vector2 Convert2LocalPoint(this RectTransform t, Vector2 uiPoint)
    //{
    //    return t.InverseTransformPoint(uiPoint * display.srceenScaleFactor);
    //}

    /// <summary>
    /// 设置轴心
    /// </summary>
    /// <param name="t"></param>
    /// <param name="anchor"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetPivot(this RectTransform t, Vector2 anchor)
    {
        t.pivot = anchor;
        return t;
    }

    /// <summary>
    /// 获取轴心
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetPivot(this RectTransform t)
    {
        return t.pivot;
    }

    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetContentSize(this RectTransform t, Vector2 size)
    {
        t.sizeDelta = size;
        return t;
    }

    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetContentSize(this RectTransform t, float width, float height)
    {
        var size = t.sizeDelta;
        size.x = width;
        size.y = height;
        t.sizeDelta = size;
        return t;
    }

    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetContentSize(this RectTransform t)
    {
        return t.sizeDelta;
    }

    /// <summary>
    /// 设置透明度
    /// </summary>
    /// <param name="t"></param>
    /// <param name="opacity">透明度，值在0-255之间</param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetOpacity(this RectTransform t, byte opacity)
    {
        // 优先判断是否有CanvasGroup，该组件会管理所有子节点的透明度
        var canvasGroup = t.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = opacity / 255f;
            return t;
        }

        var graphics = t.GetComponentsInChildren<Graphic>(true);
        foreach (var graphic in graphics)
        {
            var tmpColor = graphic.color;
            tmpColor.a = opacity / 255f;
            graphic.color = tmpColor;
        }
        return t;
    }

    /// <summary>
    /// 设置颜色
    /// </summary>
    /// <param name="t"></param>
    /// <param name="c"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetColor(this RectTransform t, Color c)
    {
        var graphics = t.GetComponentsInChildren<Graphic>(true);
        foreach (var graphic in graphics)
        {
            graphic.color = c;
        }
        return t;
    }

    /// <summary>
    /// 缩放大小
    /// </summary>
    /// <param name="t"></param>
    /// <param name="scale"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetScale(this RectTransform t, float scale)
    {
        var scaleVec = t.localScale;
        scaleVec.x = scale;
        scaleVec.y = scale;
        t.localScale = scaleVec;
        return t;
    }

    /// <summary>
    /// x、y轴缩放
    /// </summary>
    /// <param name="t"></param>
    /// <param name="sx"></param>
    /// <param name="sy"></param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetScale(this RectTransform t, float scalex, float scaley)
    {
        var scaleVec = t.localScale;
        scaleVec.x = scalex;
        scaleVec.y = scaley;
        t.localScale = scaleVec;
        return t;
    }

    /// <summary>
    /// 设置旋转角度，rotation = 0 ~ 360
    /// </summary>
    /// <param name="t"></param>
    /// <param name="rotation"> 0~360 </param>
    [UnityEngine.Scripting.Preserve]
    public static RectTransform SetRotation(this RectTransform t, float rotation)
    {
        var r = t.eulerAngles;
        r.z = rotation * -1f;
        t.eulerAngles = r;
        return t;
    }

    /// <summary>
    /// 获取矩形尺寸(锚点无关)
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector2 GetRectSize(this RectTransform t)
    {
        return t.rect.size;
    }

    /// <summary>
    /// 设置矩形尺寸(锚点无关)
    /// </summary>
    /// <param name="t"></param>
    /// <param name="size"></param>
    public static void SetRectSize(this RectTransform t, Vector2 size)
    {
        if (t.parent is RectTransform p)
        {
            Vector2 anchoredSize = (t.anchorMax - t.anchorMin) * p.GetRectSize();
            t.sizeDelta = size - anchoredSize;
        }
        else
        {
            t.sizeDelta = size;
        }
    }

    /// <summary>
    /// 创建一个空白按钮，吞噬所有点击事件，防止事件往下传递
    /// </summary>
    //[UnityEngine.Scripting.Preserve]
    //public static void SwallowTouchEvents(this RectTransform t, Action clickCallback = null)
    //{
    //    ButtonHelper.CreateSwallowButton(t, clickCallback);
    //}

    /// <summary>
    /// 常用的UI程序动画，示例：transform.Action().MoveBy(1, cc.p(50, 0)).CallFunc(()=>{}).Play();
    /// 详细接口参考CCAtion
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    //[UnityEngine.Scripting.Preserve]
    //public static CCAction BeginAction(this RectTransform t)
    //{
    //    return CCAction.Create(t);
    //}

}


