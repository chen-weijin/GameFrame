
using System;
using UnityEngine;

/// <summary>
/// 仿cocos常用方法定义
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class cc
{
    // unity提供的接口，每次都会new一个，所以最好统一使用下面接口
    public static readonly Vector2 Vector2Zero = Vector2.zero;
    public static readonly Vector3 Vector3Zero = Vector3.zero;
    public static readonly Color ColorZero = Color.clear;
    public static readonly Rect RectZero = Rect.zero;
    public static readonly Quaternion QuatZero = new Quaternion(0, 0, 0, 0);

    [UnityEngine.Scripting.Preserve]
    public static Vector2 p(float x, float y)
    {
        Vector2 res;
        res.x = x;
        res.y = y;
        return res;
    }

    [UnityEngine.Scripting.Preserve]
    public static Vector3 p3(float x, float y, float z)
    {
        Vector3 res;
        res.x = x;
        res.y = y;
        res.z = z;
        return res;
    }

    [UnityEngine.Scripting.Preserve]
    public static Quaternion q(float x, float y, float z, float w)
    {
        Quaternion q;
        q.x = x;
        q.y = y;
        q.z = z;
        q.w = w;
        return q;
    }

    /// <summary>
    /// 0~255 转 0~1
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Color c4b(int r, int g, int b, int a)
    {
        Color color;
        color.r = r / 255f;
        color.g = g / 255f;
        color.b = b / 255f;
        color.a = a / 255f;
        return color;
    }

    /// <summary>
    ///  0~255 转 0~1
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Color c3b(int r, int g, int b)
    {
        return c4b(r, g, b, 255);
    }

    [UnityEngine.Scripting.Preserve]
    public static Vector2 pAdd(Vector2 a, Vector2 b)
    {
        return a + b;
    }

    [UnityEngine.Scripting.Preserve]
    public static Vector2 pSub(Vector2 a, Vector2 b)
    {
        return a - b;
    }

    [UnityEngine.Scripting.Preserve]
    public static double pGetLength(Vector2 pos)
    {
        return Math.Sqrt(pos.x * pos.x + pos.y * pos.y);
    }

    [UnityEngine.Scripting.Preserve]
    public static double pGetDistance(Vector2 startP, Vector2 endP)
    {
        return cc.pGetLength(startP - endP);
    }

    [UnityEngine.Scripting.Preserve]
    public static Rect rect(Vector2 pos, Vector2 size)
    {
        Rect r = RectZero;
        r.position = pos;
        r.size = size;
        return r;
    }

    [UnityEngine.Scripting.Preserve]
    public static Rect rect(float x, float y, float width, float height)
    {
        Rect r = RectZero;
        r.x = x;
        r.y = y;
        r.width = width;
        r.height = height;
        return r;
    }

    [UnityEngine.Scripting.Preserve]
    public static bool rectContainsPoint(Rect rect, Vector2 point)
    {
        return rect.Contains(point);
    }

    /// <summary>
    /// 判断矩形是否相交，包含也算相交
    /// </summary>
    /// <param name="rect1"></param>
    /// <param name="rect2"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static bool rectIntersectsRect(Rect rect1, Rect rect2)
    {
        return rect1.Overlaps(rect2);
    }
}

