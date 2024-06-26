using UnityEngine;

namespace NGame
{
    /// <summary>
    /// Transform的扩展
    /// 业火
    /// </summary>
    public static class TransformUtil
    {
        /// <summary>
        /// 用Transform 来弄显隐，减少用gameObject.SetActive带来的 Rebuild
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="isVisible"></param>
        public static void SetActive(this Transform trans, bool isVisible)
        {
            if (trans == null)
                return;

            if (isVisible && !trans.gameObject.activeSelf)
                trans.gameObject.SetActive(true);

            if (isVisible == trans.IsActiveSelf()) return;

            trans.localScale = isVisible ? Vector3.one : Vector3.zero;
        }

        /// <summary>
        /// 自身是否处于显示中，要使用上面的方式进行显隐，才能调用此来判断
        /// 类似 gameObject.activeSelf
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool IsActiveSelf(this Transform trans)
        {
            return Mathf.Abs(trans.localScale.x)> 0.01f;
        }

        /// <summary>
        /// 面板上是否处于显示中，要使用上面的方式进行显隐，才能调用此来判断
        /// 类似 gameObject.activeInHierarchy
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool IsActiveInHierarchy(this Transform trans)
        {
            return trans.lossyScale.x > 0.01f;
        }

        /// <summary>
        /// 设置所有子节点的状态
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="state"></param>
        public static void SetAllChildState(this Transform trans,bool state)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                var child = trans.GetChild(i);
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
                child.gameObject.SetActive(state);
            }
        }

        /// <summary>
        /// 用Transform 来弄显隐，减少用gameObject.SetActive带来的 Rebuild
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="isVisible"></param>
        public static void SetActiveJudge(this GameObject obj, bool isVisible)
        {
            if (obj == null)
                return;

            if (isVisible == obj.activeSelf) return;

            obj.SetActive(isVisible);
        }

        /// <summary>
        /// 用Transform 来弄显隐，减少用gameObject.SetActive带来的 Rebuild
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="isVisible"></param>
        public static void SetParentJudge(this Transform obj, Transform to)
        {
            if (obj == null || to == null)
                return;

            if (obj.parent == to) return;

            obj.SetParent(to);
        }
    }
}
