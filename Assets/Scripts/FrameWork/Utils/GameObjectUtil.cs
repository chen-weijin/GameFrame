using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NGame
{
    public static class GameObjectUtil
    {
        /// <summary>
        /// Set the layer of a gameobject and all child objects
        /// </summary>
        /// <param name="o"></param>
        /// <param name="layer"></param>
        public static void SetLayerRecursive(this GameObject o, int layer)
        {
            SetLayerInternal(o.transform, layer);
        }

        private static void SetLayerInternal(Transform t, int layer)
        {
            t.gameObject.layer = layer;

            foreach (Transform o in t)
            {
                SetLayerInternal(o, layer);
            }
        }

        public static T EnsureComponent<T>(this GameObject obj) where T : Component
        {
            T r = obj.GetComponent<T>();
            if (r != null)
            {
                return r;
            }
            return obj.AddComponent<T>();
        }

        public static void SetObjActiveAndHideTransform(this GameObject obj)
        {
            if (!obj.activeSelf)
            {
                obj.gameObject.SetActive(true);
            }
            
            obj.transform.SetActive(false);
        }
    }

}
