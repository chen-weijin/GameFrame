
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Transform组件扩展
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class TransformExtension
{
    /// <summary>
    /// 获取第一层的所有节点
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static List<Transform> GetChildren(this Transform transform)
    {
        var result = new List<Transform>();
        var count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            result.Add(transform.GetChild(i));
        }
        return result;
    }

    /// <summary>
    /// 获取所有的子节点
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static List<Transform> GetAllChildren(this Transform transform)
    {
        var result = new List<Transform>();

        _AddChildren(result, transform);

        return result;
    }

    /// <summary>
    /// 移除所有子节点
    /// </summary>
    /// <param name="transform"></param>
    [UnityEngine.Scripting.Preserve]
    public static void RemoveAllChildren(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            UnityEngine.Object.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 查找sunTransform在父节点下的索引值，找不到返回-1
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static int FindChildIndex(this Transform transform, Transform child)
    {
        var count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i) == child)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 将该节点移到兄弟节点borther的上面
    /// </summary>
    /// <param name="brother"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static void MoveToAbove(this Transform transform, Transform brother)
    {
        var parent = transform.parent;
        if (parent == null)
        {
            Debug.LogWarning("错误提示：MoveToAbove父节点不存在，请检查代码");
            return;
        }

        var index = parent.FindChildIndex(brother);
        if (index == -1)
        {
            return;
        }

        // 自己如果已经在兄弟节点前面需要 - 1
        var selfIndex = parent.FindChildIndex(transform);
        var res = index < selfIndex ? index : index - 1;
        transform.SetSiblingIndex(res);
    }

    /// <summary>
    /// 将该节点移到兄弟节点borther的下面
    /// </summary>
    /// <param name="borther"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static void MoveToBelow(this Transform transform, Transform borther)
    {
        var parent = transform.parent;
        if (parent == null)
        {
            Debug.LogWarning("错误提示：MoveToBelow父节点不存在，请检查代码");
            return;
        }

        var index = parent.FindChildIndex(borther);
        if (index == -1)
        {
            return;
        }

        // 自己如果已经在兄弟节点前面需要 - 1
        var selfIndex = parent.FindChildIndex(transform);
        var res = index < selfIndex ? index + 1 : index;
        transform.SetSiblingIndex(res);
    }

    /// <summary>
    /// 将该节点移到第1个位置
    /// </summary>
    /// <param name="transform"></param>
    [UnityEngine.Scripting.Preserve]
    public static void MoveToTop(this Transform transform)
    {
        transform.SetAsFirstSibling();
    }

    /// <summary>
    /// 将该节点移到最后一个位置
    /// </summary>
    /// <param name="transform"></param>
    [UnityEngine.Scripting.Preserve]
    public static void MoveToBottom(this Transform transform)
    {
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// 查找子节点，childName可以多层级，如："FirstNodeName/SubNodeName"
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Transform FindInChildren(this Transform transform, string childName)
    {
        return transform.Find(childName);
    }

    /// <summary>
    /// 自定义条件查找子节点
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static Transform FindInChildren(this Transform transform, Func<Transform, bool> condition)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (condition(child))
            {
                return child;
            }
        }
        return null;
    }

    /// <summary>
    /// 查找子节点的组件，childName可以多层级，如："FirstNodeName/SubNodeName"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="transform"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static T FindComponent<T>(this Transform transform, string childName) where T : Component
    {
        var child = transform.Find(childName);
        if (child != null)
        {
            return child.GetComponent<T>();
        }

        return default;
    }

    /// <summary>
    /// 自定义条件查找子节点的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="transform"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static T FindComponent<T>(this Transform transform, Func<Transform, bool> condition) where T : Component
    {
        var child = transform.FindInChildren(condition);
        if (child != null)
        {
            return child.GetComponent<T>();
        }

        return default;
    }

    /// <summary>
    /// 递归查找子节点，优先建议使用FindInChildren
    /// </summary>
    /// <param name="transform">当前变换组件</param>
    /// <param name="childName">子节点物体的名称</param>
    /// <returns></returns>
    public static Transform FindReference(this Transform transform, string childName)
    {
        //1.通过子物体名称在子物体中查找变换组件
        Transform child = transform.Find(childName);
        if (child != null)
        {
            return child;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            //2.将任务交给子物体,子物体又查找子物体的子物体
            var child2 = FindReference(transform.GetChild(i), childName);
            if (child2 != null)
            {
                return child2;
            }
        }
        return null;
    }

    /// <summary>
    /// 根据自定义条件超找目标，优先建议使用FindInChildren
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static Transform FindReference(this Transform transform, Func<Transform, bool> condition)
    {
        List<Transform> children = transform.GetAllChildren();
        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            if (condition(child))
            {
                return child;
            }
        }
        return null;
    }

    [UnityEngine.Scripting.Preserve]
    public static CCAction BeginAction(this Transform t)
    {
        return CCAction.Create(t);
    }

    private static void _AddChildren(List<Transform> resultTrs, Transform curTrs)
    {
        var count = curTrs.childCount;
        for (int i = 0; i < count; i++)
        {
            var t = curTrs.GetChild(i);
            resultTrs.Add(t);

            _AddChildren(resultTrs, t);
        }
    }
}
