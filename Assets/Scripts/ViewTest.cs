using framework;
using game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WLMain;

public class ViewTest : MonoBehaviour
{
    private int _index = 0;
    private void _CreateTestBtn(string name,Action act)
    {
        var btn = UIManager.CreateObject<XTestButton>();
        btn.SetClickHandle(act);
        btn.SetName(name);
        btn.GetComponent<RectTransform>().SetParent(gameObject.GetRectTransform());
        var size = UIManager.GetRootCanvas().GetComponent<RectTransform>().GetContentSize();
        int indexMax = ((int)(size.x / btn.GetComponent<RectTransform>().sizeDelta.x)) - 1;
        var btnSize = btn.GetComponent<RectTransform>().sizeDelta;
        var startPos = new Vector2(btnSize.x / 2, -btnSize.y / 2);
        btn.GetComponent<RectTransform>().SetPosition(startPos + new Vector2(btnSize.x * (_index % indexMax), (_index / indexMax) * -btnSize.y));
        _index++;
    }
    void Start()
    {
        _CreateTestBtn("吐丝",() => {
            GameUIMgr.Instance.Spin("测试测试!!!!");
        });
        _CreateTestBtn("注册事件",() => {
            Ntfy.Instance.Add("test", (string str) => {
                Debug.Log(str);
            });
        });
        _CreateTestBtn("放送事件",() => {
            Ntfy.Instance.Post("test", "hahaha");
        });
        _CreateTestBtn("弹窗", () => {
            GameUIMgr.Instance.PopNormal();
        });
        _CreateTestBtn("音效", () => {
            AudioMgr.Instance.PlayEffect("audio/effect/lineBomb2");
        });
        _CreateTestBtn("写缓存", () => {
            CacheMgr.GetInstance().SetItem("test", "ffff");
        });
        _CreateTestBtn("读缓存", () => {
            string str = CacheMgr.GetInstance().GetItem("test");
            Debug.Log(str);
        });
        _CreateTestBtn("读csv数据", () => {
            var data = TableMgr.GetInstance().PlayerData;
            for(var i = 0; i < data.Count; i++)
            {
                var d = data[i];
                Debug.Log("属性: "+d.Name+"__" +d.Age + "__" + d.Score); // Debug.Log(f.Name);
            }
        });
        //吐丝 //已完成
        //事件机制 //已完成
        //弹窗 //已完成
        //音效 //已完成
        //本地缓存 //已完成
        //数值配置 //已完成
        //列表
        //网络

    }

}
