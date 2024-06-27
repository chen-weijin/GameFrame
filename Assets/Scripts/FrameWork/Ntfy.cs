using framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WLMain
{
    /// <summary>
    /// czw - 简单事件处理
    /// </summary>
    public class Ntfy : Singleton<Ntfy>
    {
        private Dictionary<string, List<Delegate>> _dict = new(128);

        public void Add(string eventName, Action callback)
        {
            AddInternal(eventName, callback);
        }

        public void Add<T1>(string eventName, Action<T1> callback)
        {
            AddInternal(eventName, callback);
        }

        public void Add<T1, T2>(string eventName, Action<T1, T2> callback)
        {
            AddInternal(eventName, callback);
        }

        public void Add<T1, T2, T3>(string eventName, Action<T1, T2, T3> callback)
        {
            AddInternal(eventName, callback);
        }

        private void AddInternal(string eventName, Delegate callback)
        {
            _dict.TryGetValue(eventName, out List<Delegate> ls);
            if (ls == null)
            {
                ls = new() { callback };
                _dict[eventName] = ls;
            }
            else
            {
                if (ls.IndexOf(callback) < 0)
                    ls.Add(callback);
            }
        }

        public void Remove(string eventName, Action callback)
        {
            Delegate tempDelegate = callback;
            RemoveInternal(eventName, tempDelegate);
        }

        public void Remove<T1>(string eventName, Action<T1> callback)
        {
            Delegate tempDelegate = callback;
            RemoveInternal(eventName, tempDelegate);
        }

        public void Remove<T1, T2>(string eventName, Action<T1, T2> callback)
        {
            Delegate tempDelegate = callback;
            RemoveInternal(eventName, tempDelegate);
        }

        public void Remove<T1, T2, T3>(string eventName, Action<T1, T2, T3> callback)
        {
            Delegate tempDelegate = callback;
            RemoveInternal(eventName, tempDelegate);
        }

        private void RemoveInternal(string eventName, Delegate callback)
        {
            _dict.TryGetValue(eventName, out List<Delegate> ls);
            if (ls == null)
                return;

            ls.Remove(callback);
            if (ls.Count == 0)
                _dict.Remove(eventName);
        }

        public void Post(string eventName)
        {
            _dict.TryGetValue(eventName, out List<Delegate> ls);
            if (ls == null)
            {
                Debug.LogWarning($"[Ntfy] Post, 事件:{eventName} 未监听");
                return;
            }

            int len = ls.Count;
            for (int i = len - 1; i >= 0; --i)
            {
                var callback = ls[i];
                if (callback != null)
                {
                    if (callback is Action)
                    {
                        try
                        {
                            ((Action)callback)();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[Ntfy] Post, 事件:{eventName} 回调处理异常:{e}");
                        }
                    }
                    else Debug.LogError($"[Ntfy] Post, 事件:{eventName}，回调方法:{callback?.Method?.Name}, 错误的委托类型:{callback?.ToString()}");
                }
            }
        }

        public void Post<T>(string eventName, T t)
        {
            _dict.TryGetValue(eventName, out List<Delegate> ls);
            if (ls == null)
            {
                Debug.LogWarning($"[Ntfy] Post, 事件:{eventName} 未监听");
                return;
            }

            int len = ls.Count;
            for (int i = len - 1; i >= 0; --i)
            {
                var callback = ls[i];
                if (callback != null)
                {
                    if (callback is Action<T>)
                    {
                        try
                        {
                            ((Action<T>)callback)(t);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[Ntfy] Post, 事件:{eventName} 回调处理异常:{e}");
                        }
                    }
                    else Debug.LogError($"[Ntfy] Post, 事件:{eventName}，回调方法:{callback?.Method?.Name}, 错误的委托类型:{callback?.ToString()}");
                }
            }
        }

        public void Post<T1, T2>(string eventName, T1 t1, T2 t2)
        {
            _dict.TryGetValue(eventName, out List<Delegate> ls);
            if (ls == null)
            {
                Debug.LogWarning($"[Ntfy] Post, 事件:{eventName} 未监听");
                return;
            }

            int len = ls.Count;
            for (int i = len - 1; i >= 0; --i)
            {
                var callback = ls[i];
                if (callback != null)
                {
                    if (callback is Action<T1, T2>)
                    {
                        try
                        {
                            ((Action<T1, T2>)callback)(t1, t2);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[Ntfy] Post, 事件:{eventName} 回调处理异常:{e}");
                        }
                    }
                    else
                        Debug.LogError($"[Ntfy] Post, 事件:{eventName}，回调方法:{callback?.Method?.Name}, 错误的委托类型:{callback?.ToString()}");
                }
            }
        }

        public void Post<T1, T2, T3>(string eventName, T1 t1, T2 t2, T3 t3)
        {
            _dict.TryGetValue(eventName, out List<Delegate> ls);
            if (ls == null)
            {
                Debug.LogWarning($"[Ntfy] Post, 事件:{eventName} 未监听");
                return;
            }

            int len = ls.Count;
            for (int i = len - 1; i >= 0; --i)
            {
                var callback = ls[i];
                if (callback != null)
                {
                    if (callback is Action<T1, T2, T3>)
                    {
                        try
                        {
                            ((Action<T1, T2, T3>)callback)(t1, t2, t3);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[Ntfy] Post, 事件:{eventName} 回调处理异常:{e}");
                        }
                    }
                    else
                        Debug.LogError($"[Ntfy] Post, 事件:{eventName}，回调方法:{callback?.Method?.Name}, 错误的委托类型:{callback?.ToString()}");
                }
            }
        }


    }
}
