using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace framework
{
    /// <summary>
    /// 界面管理器。
    /// </summary>
    public  class UIManager : MonoSingleton<UIManager>
    {


        public static T CreateObject<T>() where T : MonoBehaviour
        {
            var prefabPath = GetPrefabPath<T>();
            var prefabName = typeof(T).Name;
            T go = CreateObject<T>(prefabPath);
            go.name = prefabName;
            return go;
        }

        public static GameObject CreateObject(string prefabPath)
        {
            var go = InstantiatePrefab(prefabPath);
            go.name = go.name.Replace("(Clone)", "");
            return go;
        }
        public static T CreateEmptyObject<T>() where T : MonoBehaviour
        {
            var go = new GameObject();
            go.AddComponent<RectTransform>();
            go.name = typeof(T).Name;
            return go.AddComponent<T>();
        }

        public static T CreateLayer<T>() where T : MonoBehaviour
        {
            var prefabPath = GetPrefabPath<T>();
            var prefabName = typeof(T).Name;
            var com = CreateObject<T>(prefabPath);
            com.name = prefabName;

            SetFullScreen(com);
            return com;
        }

        public static T CreateEmptyLayer<T>() where T : MonoBehaviour
        {
            var com = CreateEmptyObject<T>();
            com.name = typeof(T).Name;

            SetFullScreen(com);
            return com;
        }

        public static void SetSortingOrder(MonoBehaviour layer, int order)
        {
            if (order < 0) return;
            var canvas = layer.EnsureComponent<Canvas>();
            layer.EnsureComponent<GraphicRaycaster>();

            canvas.overrideSorting = true;
            canvas.sortingOrder = order;
        }
        private static void SetFullScreen(MonoBehaviour layer)
        {
            var rect = layer.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }


        private static T CreateObject<T>(string prefabPath) where T : MonoBehaviour
        {

            var go = InstantiatePrefab(prefabPath);
            go.name = go.name.Replace("(Clone)", "");


            T r = go.GetComponent<T>();
            if (r != null)
            {
                return r;
            }
            return go.AddComponent<T>();
        }

        // 假设预制体位于名为"Resources/Prefabs"的文件夹中
        public static GameObject InstantiatePrefab(string prefabPath)
        {
            // 注意：prefabPath在这里只是预制体的名字，不包括".prefab"扩展名，也不包括"Resources/"前缀  
            // 例如，如果预制体的完整路径是"Assets/Resources/Prefabs/MyPrefab.prefab"，则prefabPath应为"Prefabs/MyPrefab"  
            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (prefab == null)
            {
                Debug.LogError("无法加载预制体：" + prefabPath);
                return null;
            }

            // 实例化预制体并返回新的GameObject实例  
            return Instantiate(prefab);
        }

        private const string Prefab_Path_Key = "Prefab_Path";
        private static string GetPrefabPath<T>()
        {
            var type = typeof(T);
            var prefabPathField = type.GetField(Prefab_Path_Key);

            if (prefabPathField == null)
            {
                Debug.LogError($"类{type}未定义常量变量Prefab_Path，请检查代码");
                return string.Empty;
            }

            var prefabPath = prefabPathField.GetValue("") as string;
            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError($"类{type} Prefab_Path值为空，请检查代码");
                return string.Empty;
            }

            return prefabPath;
        }


        public static Transform GetRootCanvas()
        {
            var s= SceneManager.GetActiveScene();
            var gArr = s.GetRootGameObjects();
            for(var i = 0; i < gArr.Length; i++)
            {
                if (gArr[i].name == "Canvas")
                {
                    return gArr[i].transform;
                }
            }
            return null;
            //var uiGroup = (UIGroup)Instance.GetUIGroup(UIGroupLayer.ViewLayer.ToString());
            //var parent = uiGroup.Helper.transform;
            //RectTransform root = parent.Find("GameUIRoot") as RectTransform;
            //if (root == null)
            //{
            //    var obj = new GameObject("GameUIRoot");
            //    root = obj.AddComponent<RectTransform>();
            //    root.SetParent(parent, false);
            //    root.anchorMin = Vector2.zero;
            //    root.anchorMax = Vector2.one;
            //    root.offsetMin = Vector2.zero;
            //    root.offsetMax = Vector2.zero;
            //    root.pivot = new Vector2(0.5f, 0.5f);
            //}
            //return root;
        }
    }


}
