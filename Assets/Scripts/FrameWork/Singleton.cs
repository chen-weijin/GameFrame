using System;

namespace framework
{
    /// <summary>
    /// 改自 fsbm
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance = null;

        /// <summary>
        ///  获取单例
        /// </summary>
        /// 
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new T();
                        _instance.Init();
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError("Singleton Instance Type=" + typeof(T).ToString() + ",e=" + e.Message);
                    }
                }

                return _instance;
            }
        }
       
        /// <summary>
        /// 消毁单例
        /// </summary>
        public static void DestroyInstance()
        {
            if (_instance != null)
            {
                try { _instance.Destroy(); } catch { }
                _instance = null;
            }
        }

        protected Singleton()
        {
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
        }

        /// <summary>
        /// 消毁响应
        /// </summary>
        protected virtual void OnDestroy()
        {
        }

        /// <summary>
        /// 主动消毁
        /// </summary>
        public virtual void Destroy()
        {
            OnDestroy();
        }
    }
}
