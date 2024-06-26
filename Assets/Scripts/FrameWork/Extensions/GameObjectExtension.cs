
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject功能扩展
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class GameObjectExtension
{
    [UnityEngine.Scripting.Preserve]
    public static RectTransform GetRectTransform(this GameObject obj)
    {
        return obj.transform as RectTransform;
    }

    [UnityEngine.Scripting.Preserve]
    public static void SetParent(this GameObject obj, GameObject parent)
    {
        obj.transform.SetParent(parent.transform, false);
    }

    [UnityEngine.Scripting.Preserve]
    public static void SetParent(this GameObject obj, Transform parent)
    {
        obj.transform.SetParent(parent, false);
    }

    /// <summary>
    /// 获取第一层的所有子节点
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static List<Transform> GetChildren(this GameObject obj)
    {
        return obj.transform.GetChildren();
    }

    /// <summary>
    /// 获取所有子节点
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static List<Transform> GetAllChildren(this GameObject obj)
    {
        return obj.transform.GetAllChildren();
    }

    /// <summary>
    /// 查找子节点，childName可以多层级，如："FirstNodeName/SubNodeName"
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Transform FindInChildren(this GameObject obj, string childName)
    {
        return obj.transform.FindInChildren(childName);
    }

    /// <summary>
    /// 详细见TransformExtension.FindInChildren
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Transform FindInChildren(this GameObject obj, Func<Transform, bool> condition)
    {
        return obj.transform.FindInChildren(condition);
    }

    /// <summary>
    /// 查找子节点的组件，childName可以多层级，如："FirstNodeName/SubNodeName"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static T FindComponent<T>(this GameObject obj, string childName) where T : Component
    {
        return obj.transform.FindComponent<T>(childName);
    }

    /// <summary>
    /// 详细见TransformExtension.FindInChildren
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static T FindComponent<T>(this GameObject obj, Func<Transform, bool> condition) where T : Component
    {
        return obj.transform.FindComponent<T>(condition);
    }

    /// <summary>
    /// 组件如果存在则直接返回，否则创建一个新的，确保组件一定存在
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static T EnsureComponent<T>(this GameObject obj) where T : Component
    {
        T r = obj.GetComponent<T>();
        if (r != null)
        {
            return r;
        }
        return obj.AddComponent<T>();
    }

    /// <summary>
    /// 设置层
    /// </summary>
    /// <param name="o"></param>
    /// <param name="layer"></param>
    /// <param name="recuresive">是否递归应用到所有自节点</param>
    [UnityEngine.Scripting.Preserve]
    public static void SetLayer(this GameObject o, int layer, bool recuresive = false)
    {
        if (recuresive)
        {
            SetLayerInternal(o.transform, layer);
        }
        else
        {
            o.layer = layer;
        }
    }

    private static void SetLayerInternal(Transform t, int layer)
    {
        t.gameObject.layer = layer;

        foreach (Transform o in t)
        {
            SetLayerInternal(o, layer);
        }
    }
}
