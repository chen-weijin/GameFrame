
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// GameObject功能扩展
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class MonoBehaviourExtension
{
    [UnityEngine.Scripting.Preserve]
    public static void SetParent(this MonoBehaviour obj, GameObject parent, bool worldPositionStays = false)
    {
        obj.transform.SetParent(parent.transform, worldPositionStays);
    }

    [UnityEngine.Scripting.Preserve]
    public static void SetParent(this MonoBehaviour obj, Transform parent, bool worldPositionStays = false)
    {
        obj.transform.SetParent(parent, worldPositionStays);
    }

    /// <summary>
    /// 获取第一层的所有子节点
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static List<Transform> GetChildren(this MonoBehaviour obj)
    {
        return obj.transform.GetChildren();
    }

    /// <summary>
    /// 获取所有子节点
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static List<Transform> GetAllChildren(this MonoBehaviour obj)
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
    public static Transform FindInChildren(this MonoBehaviour obj, string childName)
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
    public static Transform FindInChildren(this MonoBehaviour obj, Func<Transform, bool> condition)
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
    public static T FindComponent<T>(this MonoBehaviour obj, string childName) where T : Component
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
    public static T FindComponent<T>(this MonoBehaviour mono, Func<Transform, bool> condition) where T : Component
    {
        return mono.transform.FindComponent<T>(condition);
    }

    /// <summary>
    /// 组件如果存在则直接返回，否则创建一个新的，确保组件一定存在
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static T EnsureComponent<T>(this MonoBehaviour mono) where T : Component
    {
        return mono.gameObject.EnsureComponent<T>();
    }

    /// <summary>
    /// 设置层
    /// </summary>
    /// <param name="o"></param>
    /// <param name="layer"></param>
    /// <param name="recuresive">是否递归应用到所有自节点</param>
    [UnityEngine.Scripting.Preserve]
    public static void SetLayer(this MonoBehaviour o, int layer, bool recuresive = false)
    {
        o.gameObject.SetLayer(layer, recuresive);
    }

    /// <summary>
    /// 删除自身GameObject对象
    /// </summary>
    /// <param name="o"></param>
    [UnityEngine.Scripting.Preserve]
    public static void RemoveSelf(this MonoBehaviour o)
    {
        GameObject.Destroy(o.gameObject);
    }

    /// <summary>
    /// 延迟时间执行，对象被删除回调也会跟着删除
    /// </summary>
    /// <param name="o"></param>
    /// <param name="duration"></param>
    /// <param name="cb"></param>
    [UnityEngine.Scripting.Preserve]
    public static void DelayCall(this MonoBehaviour o, float duration, Action cb)
    {
        o.StartCoroutine(_DelayCall(duration, cb));
    }

    private static IEnumerator _DelayCall(float d, Action cb)
    {
        yield return new WaitForSeconds(d);
        cb();
    }

    /// <summary>
    /// 重复次数执行，对象被删除回调也会跟着删除
    /// </summary>
    /// <param name="o"></param>
    /// <param name="duration"></param>
    /// <param name="repeatCount"> 该值必须 > 0，每次都会new所以不想支持无限循环 </param>
    /// <param name="cb"> 回调参数当前次数，索引从0开始 </param>
    [UnityEngine.Scripting.Preserve]
    public static void RepeatCall(this MonoBehaviour o, float duration, int repeatCount, Action<int> cb)
    {
        Debug.Assert(repeatCount > 0, $"repeatCount need > 0");

        o.StartCoroutine(_RepeatCall(duration, repeatCount, cb));
    }

    private static IEnumerator _RepeatCall(float duration, int repeatCount, Action<int> cb)
    {
        for (int i=0; i<repeatCount; i++)
        {
            yield return new WaitForSeconds(duration);
            cb(i);
        }
    }

    /// <summary>
    /// 内部自己用，一般用不上
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="root"></param>
    /// <returns></returns>
    public static Sequence CreateTweenGraphSequence(this Transform transform, DORootNode root)
    {
        var sequence = DOTween.Sequence();
        var tweenNode = root.GetOutputPort("next").Connection.node as DOBaseNode;
        while (tweenNode != null)
        {
            var tween = tweenNode.GenerateTween(transform);
            if (tween != null)
            {
                sequence.Append(tween);
            }
            tweenNode = tweenNode.GetNextNode();
        }
        sequence.SetTarget(transform.gameObject);
        sequence.SetLink(transform.gameObject);
        sequence.SetAutoKill(true);
        return sequence;
    }
}
