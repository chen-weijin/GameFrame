
using System;
using UnityEngine;

/// <summary>
/// 适配后的屏幕信息，如果需要调整UI位置大小，使用该接口才是正确的数值
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class display
{
    /// <summary>
    /// 屏幕尺寸
    /// </summary>
    public static Vector2 size;
    /// <summary>
    /// 屏幕宽度
    /// </summary>
    public static float width = 0;
    /// <summary>
    /// 屏幕高度
    /// </summary>
    public static float height = 0;
    /// <summary>
    /// 屏幕中心点x坐标
    /// </summary>
    public static float cx = 0;
    /// <summary>
    /// 屏幕中心点y坐标
    /// </summary>
    public static float cy = 0;
    /// <summary>
    /// 屏幕适配缩放比例
    /// </summary>
    public static float srceenScaleFactor = 0f;
    /// <summary>
    /// 屏幕宽高比
    /// </summary>
    public static float aspect = 0;
    /// <summary>
    /// 屏幕中心点坐标
    /// </summary>
    public static Vector2 center;
    /// <summary>
    /// 屏幕左上角坐标
    /// </summary>
    public static Vector2 left_top;
    /// <summary>
    /// 屏幕左下角坐标
    /// </summary>
    public static Vector2 left_bottom;
    /// <summary>
    /// 屏幕左中坐标
    /// </summary>
    public static Vector2 left_center;
    /// <summary>
    /// 屏幕右上角坐标
    /// </summary>
    public static Vector2 right_top;
    /// <summary>
    /// 屏幕右下角坐标
    /// </summary>
    public static Vector2 right_bottom;
    /// <summary>
    /// 屏幕右中坐标
    /// </summary>
    public static Vector2 right_center;
    /// <summary>
    /// 屏幕顶中坐标
    /// </summary>
    public static Vector2 top_center;
    /// <summary>
    /// 屏幕底中坐标
    /// </summary>
    public static Vector2 top_bottom;
    /// <summary>
    /// 小游戏那条横幅的大小，非小游戏返回0，如果是屏幕右上角适配需要扣除bar的大小
    /// </summary>
    public static Vector2 wx_bar = new Vector2(0, 0);

    /// <summary>
    /// 需要在游戏启动后初始化1次
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static void Init()
    {
        var designSize = DesignResolution.GetDesignSize();
        var scrSize = DesignResolution.GetScreenSize();
        float sx = scrSize.x / designSize.x;
        float sy = scrSize.y / designSize.y;
        float scale = DesignResolution.Is2to1Screen() ? sy : sx;    // 太宽则等高适配否则等宽适配
        display.width = scrSize.x / scale;
        display.height = scrSize.y / scale;
        display.srceenScaleFactor = scale;
        display.aspect = scrSize.x / scrSize.y;
        display.size = cc.p(display.width, display.height);
        display.cx = display.width / 2;
        display.cy = display.height / 2;
        display.center = cc.p(display.width / 2, display.height / 2);
        display.left_top = cc.p(0, display.height);
        display.left_bottom = cc.p(0, 0);
        display.left_center = cc.p(0, display.height / 2);
        display.right_top = cc.p(display.width, display.height);
        display.right_bottom = cc.p(display.width, 0);
        display.right_center = cc.p(display.width, display.height / 2);
        display.top_center = cc.p(display.width / 2, display.height);
        display.top_bottom = cc.p(display.width / 2, 0);

#if UNITY_WX_MINIGAME
        var info = WXSystemInfoHelper.GetSystemInfo();
        var barWidth = display.width / (float)info.screenWidth * 100;   // 100x44是微信宽高
        var barHeight = display.height / (float)info.screenHeight * 44;
        wx_bar = cc.p(barWidth, barHeight);
#endif
    }

#if UNITY_EDITOR
    // 每次运行时需要重置静态变量
    //[System.Diagnostics.Conditional("UNITY_EDITOR")]
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Reset()
    {
        display.width = 0;
        display.height = 0;
        display.srceenScaleFactor = 0;
        display.aspect = 0;
        display.size = Vector2.zero;
        display.cx = 0;
        display.cy = 0;
        display.center = Vector2.zero;
        display.left_top = Vector2.zero;
        display.left_bottom = Vector2.zero;
        display.left_center = Vector2.zero;
        display.right_top = Vector2.zero;
        display.right_bottom = Vector2.zero;
        display.right_center = Vector2.zero;
        display.top_center = Vector2.zero;
        display.top_bottom = Vector2.zero;
    }
#endif
}
