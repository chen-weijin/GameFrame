
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 适配相关逻辑
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class DesignResolution
{
    // 适配微信小游戏右上角，按钮是向左还是向下适配
    public enum WXBarOrient
    {
        ToLeft,
        ToDown,
    }

    private static Vector2 _designSize = new Vector2(1920, 1080);
    private static Vector2 _screenOffset = Vector2.zero;

    /// <summary>
    /// 初始适配模式，屏幕比超过2:1则按等高适配(如iPhoneX)，否则按等宽适配(如iPad)
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static void Init()
    {
        var obj = GameObject.Find("UICanvas");
        if (obj == null)
        {
            Debug.LogError($"规范：请在Scene场景根节点创建名为UICanvas的UI基础组件，且必须带上Canvas Scaler组件");
            return;
        }

        var canvasScaler = obj.GetComponent<CanvasScaler>();
        Debug.Assert(canvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize, "Canvas Scaler组件的UI Scale Mode规范必须是Scale With Screen Size模式");

        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _designSize = canvasScaler.referenceResolution;
        if ((_designSize.x == 1920 && _designSize.y == 1080) == false)
        {
            Debug.LogWarning($"警告：当前设计分辨率为{_designSize.x}x{_designSize.y}，规范分辨率为1920x1080，请修改");
        }
        Debug.Log($"当前UI设计分辨率为：{_designSize}");

        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        if (IsPortrai())
        {
            // 竖屏，等宽
            canvasScaler.matchWidthOrHeight = Is2to1Screen() ? 0f : 1f;
        }
        else
        {
            // 横屏，等高
            canvasScaler.matchWidthOrHeight = Is2to1Screen() ? 1f : 0f;
        }

        // 默认长宽屏左右按钮适配，左右各空64 * 1.5 = 96像素
        if (IsSuper2to1Screen())
        {
            SetScreenOffset(96, 96);
        }
    }

    /// <summary>
    /// 自定义宽屏手机左右两边偏移位置，比如iPhone X当刘海在左边则（96, 0），刘海在右边则（0, 96）
    /// 如果是竖屏则表示
    /// </summary>
    /// <param name="l"></param>
    /// <param name="r"></param>
    [UnityEngine.Scripting.Preserve]
    public static void SetScreenOffset(float l, float r)
    {
        _screenOffset = cc.p(l, r);
    }

    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetScreenOffset()
    {
        return _screenOffset;
    }

    /// <summary>
    /// 获取屏幕大小，确保竖屏宽<高，横屏宽>高
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetScreenSize()
    {
        if (IsPortrai())
        {
            return Screen.width > Screen.height ?
                cc.p(Screen.height, Screen.width) :
                cc.p(Screen.width, Screen.height);
        }
        return Screen.width > Screen.height ?
                cc.p(Screen.width, Screen.height) :
                cc.p(Screen.height, Screen.width);
    }

    /// <summary>
    /// 默认设计分辨率大小，比率1.777。项目组可根据自己需求修改
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetDesignSize()
    {
        Debug.Assert(_designSize.x != 0 && _designSize.y != 0, "请先InitLogic.Init初始设计分辨率的值");
        return _designSize;
    }

    /// <summary>
    /// 是否是竖屏，判断标准为UICanvas -> CanvasScaler组件的属性referenceResolution x<y表示竖屏，否则横屏
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static bool IsPortrai()
    {
        Debug.Assert(_designSize.x != 0 && _designSize.y != 0, "请先InitLogic.Init初始设计分辨率的值");
        return _designSize.x < _designSize.y;
    }

    /// <summary>
    /// 是否2 ：1的屏幕
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static bool Is2to1Screen()
    {
        // 1.85经验数值，超过这个值表示2:1的屏幕
        return GetFactor() > 1.85f;
    }

    /// <summary>
    /// 是否超宽屏，如iPhoneX
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static bool IsSuper2to1Screen()
    {
        // 2.07经验数值
        return GetFactor() > 2.07;
    }

    /// <summary>
    /// 根据适配值，返回是否要加上刘海屏的宽度
    /// </summary>
    /// <param name="layout"> UI9点适配值，如(0.5f, 0) </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetScreenOffset(Vector2 layout)
    {
        // 竖屏
        if (IsPortrai())
        {
            // 在顶部位置则减去适配
            if (layout.y == 1) return cc.p(0, -_screenOffset.x);

            // 在底部位置则加上适配大小
            if (layout.y == 0) return cc.p(_screenOffset.y, 0);
            return cc.Vector2Zero;
        }

        // 横屏
        if (layout.x == 0)
        {
            return cc.p(_screenOffset.x, 0);
        }
        else if (layout.x == 1)
        {
            return cc.p(-_screenOffset.y, 0);
        }
        return cc.Vector2Zero;
    }

    /// <summary>
    /// 根据9点适配值，返回刘海屏偏移值。适配微信右上角
    /// </summary>
    /// <param name="layout"> UI9点适配值，如(0.5f, 0) </param>
    /// <param name="orient"> 微信小游戏右上角，往左还是往下适配 </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Vector2 GetScreenOffset(Vector2 layout, WXBarOrient orient)
    {
        // 微信小游戏右上角需要特殊处理
        //if (PlatformHelper.IsWXMiniGame() &&
        //    layout == Vector2.one)
        //{
        //    var offset = DesignResolution.GetScreenOffset(layout);
        //    if (orient == WXBarOrient.ToLeft)
        //    {
        //        offset.x -= display.wx_bar.x;
        //        return offset;
        //    }
        //    else
        //    {
        //        offset.y -= display.wx_bar.y;
        //        return offset;
        //    }
        //}

        return DesignResolution.GetScreenOffset(layout);
    }

    /// <summary>
    /// 设置渲染屏幕比，性能差的手机降低比例对于性能帮助很大，一般情况：
    /// 高质量：1.0；中：0.85；低质量：0.7
    /// 备注：WebGL平台(微信小游戏)无法设置
    /// </summary>
    /// <param name="ratio"></param>
    [UnityEngine.Scripting.Preserve]
    public static void SetScreenResolution(float ratio)
    {
#if UNITY_WEBGL
        // 该平台无法使用
        return;
#else

        if (ratio >= 1)
        {
            return;
        }

        int scaleWidth = Mathf.FloorToInt(Screen.currentResolution.width * ratio);
        int scaleHeight = Mathf.FloorToInt(Screen.currentResolution.height * ratio);
#if UNITY_STANDALONE
        Screen.SetResolution(scaleWidth, scaleHeight, false); // 不允许全屏
#else
        Screen.SetResolution(scaleWidth, scaleHeight, true);
#endif
        Debug.Log($"已重置当前屏幕分辨率：{scaleWidth}x{scaleWidth}");
#endif

    }

    /// <summary>
    /// 屏幕分辨率比，值一定>0
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static float GetFactor()
    {
        float width = Screen.width;
        float height = Screen.height;
        return width > height ? width / height : height / width;
    }
}
