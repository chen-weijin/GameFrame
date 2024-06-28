using framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    public class XTestButton : MonoBehaviour
    {
        public const string Prefab_Path = "prefabs/btn";
        public const string Prefab_Name = "TestBtn";

        private Action _clickHandle;

        public static XTestButton Create(Action act)
        {
            var btn = UIManager.CreateObject<XTestButton>();
            btn.SetClickHandle(act);
            return btn;
        }
        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(_OnClick);
        }
        public void SetClickHandle(Action act)
        {
            _clickHandle = act;
        }

        private void _OnClick()
        {
            if (_clickHandle == null) return;
            _clickHandle();
        }
    }
}