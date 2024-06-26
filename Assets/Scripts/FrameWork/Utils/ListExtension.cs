// @Author: tanjinhua
// @Date: 2021/4/29  20:15


using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 列表功能扩展
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// 获取列表第一个或末尾元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Peek<T>(this List<T> list, bool rear = false)
    {
        if (list.Count == 0)
        {
            return default;
        }

        var index = rear ? list.Count - 1 : 0;
        return list[index];
    }

    /// <summary>
    /// 移除第一个元素并返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T Pop<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }

        var item = list[0];
        list.RemoveAt(0);
        return item;
    }

    /// <summary>
    /// 分类排序，把满足条件的元素放在列表前端
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition"></param>
    public static void Classify<T>(this List<T> list, Func<T, bool> condition)
    {
        List<T> satisfied = new List<T>();
        List<T> unsatisfied = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            T v = list[i];
            if (condition(v))
            {
                satisfied.Add(v);
            }
            else
            {
                unsatisfied.Add(v);
            }
        }

        list.Clear();

        list.AddRange(satisfied);

        list.AddRange(unsatisfied);
    }

    /// <summary>
    /// 提取。把满足条件的元素放到一个新列表中返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static List<T> Fetch<T>(this List<T> list, Func<T, bool> condition)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < list.Count; i++)
        {
            T item = list[i];
            if (condition(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    /// <summary>
    /// 根据自定义条件获取元素索引，找到第一个后马上返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static int GetIndexByCondition<T>(this List<T> list, Func<T, bool> condition)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (condition(list[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 获取多个元素的索引列表，每个索引只找一次
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static List<int> IndicesOf<T>(this List<T> list, List<T> items)
    {
        List<int> result = new List<int>();

        items.ForEach(item =>
        {
            int index = -1;

            for (int i = 0; i < list.Count; i++)
            {
                if (item.Equals(list[i]) && !result.Contains(i))
                {
                    index = i;
                    break;
                }
            }

            result.Add(index);
        });

        return result;
    }

    /// <summary>
    /// 克隆
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<T> Clone<T>(this List<T> list)
    {
        return new List<T>(list);
    }

    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static int Count<T>(this List<T> list, T item)
    {
        int result = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(item))
            {
                result++;
            }
        }

        return result;
    }

    /// <summary>
    /// 根据条件计数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static int CountByCondition<T>(this List<T> list, Func<T, bool> condition)
    {
        int result = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (condition(list[i]))
            {
                result++;
            }
        }

        return result;
    }

    /// <summary>
    /// 根据条件删除元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition"></param>
    public static void RemoveByCondition<T>(this List<T> list, Func<T, bool> condition)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (condition(list[i]))
            {
                list.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 切片
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static List<T> Slice<T>(this List<T> list, int start, int end)
    {
        List<T> result = new List<T>();

        for (int i = start; i < end; i++)
        {
            result.Add(list[i]);
        }

        return result;
    }

    /// <summary>
    /// 交错列表平铺
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="crossList"></param>
    /// <returns></returns>
    public static List<T> Tile<T>(this List<List<T>> crossList)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < crossList.Count; i++)
        {
            List<T> list = crossList[i];

            for (int j = 0; j > list.Count; j++)
            {
                result.Add(list[j]);
            }
        }

        return result;
    }

    /// <summary>
    /// 检查索引是否有效
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    public static bool IndexValid<T>(this List<T> list, int index, string errorMsg = null)
    {
        bool valid = index >= 0 && index < list.Count;

        if (!valid && !string.IsNullOrEmpty(errorMsg))
        {
            Debug.LogWarning(errorMsg);
        }

        return valid;
    }

    /// <summary>
    /// 是否与传入的列表具有相同元素(顺序无关)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool HasSameElements<T>(this List<T> list, List<T> other) where T : IComparable
    {
        if (list.Count != other.Count)
        {
            return false;
        }

        List<T> thisCopy = new List<T>(list);
        List<T> otherCopy = new List<T>(other);
        thisCopy.Sort((a, b) => a.CompareTo(b));
        otherCopy.Sort((a, b) => a.CompareTo(b));

        for (int i = 0; i < thisCopy.Count; i++)
        {
            if (!thisCopy[i].Equals(otherCopy[i]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 两个列表的元素是否完全一样
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool IsSameList<T>(this List<T> list, List<T> other)
    {
        if (list == null || other == null)
        {
            return false;
        }

        if (list.Count != other.Count)
        {
            return false;
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].Equals(other[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static List<T> Unpack<T>(this List<List<T>> list)
    {
        var newArr = new List<T>();
        for (var i = 0; i < list.Count; i++)
        {
            var value = list[i];
            int valueLen = value == null ? 0 : value.Count;
            for (var j = 0; j < valueLen; j++)
            {
                newArr.Add(value[j]);
            }
        }
        return newArr;
    }


    /// <summary>
    /// 删除list内包含other的所有数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="other"></param>
    [UnityEngine.Scripting.Preserve]
    public static void RemoveContains<T>(this List<T> list, List<T> other)
    {
        foreach (var item in other)
        {
            list.Remove(item);
        }
    }
}
