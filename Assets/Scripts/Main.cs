using framework;
using game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WLMain;

public class Main : MonoBehaviour
{
    private int _index = 0;
    private void _CreateTestBtn(Action act)
    {
        var btn = XTestButton.Create(act);
        var size = UIManager.GetRootCanvas().GetComponent<RectTransform>().GetContentSize();
        int indexMax = ((int)(size.x / btn.GetComponent<RectTransform>().sizeDelta.x)) - 1;
        var btnSize = btn.GetComponent<RectTransform>().sizeDelta;
        var startPos = new Vector2(btnSize.x / 2, -btnSize.y / 2);
        btn.GetComponent<RectTransform>().SetPosition(startPos+new Vector2(btnSize.x * (_index % indexMax),(_index / indexMax) * -btnSize.y));
        _index++;
    }
    void Start()
    {
        _CreateTestBtn(() => {
            ViewToshi.Create("测试测试!!!!");//吐丝测试
        });
        _CreateTestBtn(() => {
            Ntfy.Instance.Add("test", (string str) => {
                Debug.Log(str);
            });
        });
        _CreateTestBtn(() => {
            Ntfy.Instance.Post("test", "hahaha");
        });
        _CreateTestBtn(() => {
            ViewPopNormal.Create();
        });
        //吐丝 //已完成
        //事件机制 //已完成
        //弹窗 //已完成
        //列表
        //网络
        //
    }

}
