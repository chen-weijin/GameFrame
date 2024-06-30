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
        _CreateTestBtn("��˿",() => {
            GameUIMgr.Instance.Spin("���Բ���!!!!");
        });
        _CreateTestBtn("ע���¼�",() => {
            Ntfy.Instance.Add("test", (string str) => {
                Debug.Log(str);
            });
        });
        _CreateTestBtn("�����¼�",() => {
            Ntfy.Instance.Post("test", "hahaha");
        });
        _CreateTestBtn("����", () => {
            GameUIMgr.Instance.PopNormal();
        });
        _CreateTestBtn("��Ч", () => {
            AudioMgr.Instance.PlayEffect("audio/effect/lineBomb2");
        });
        _CreateTestBtn("д����", () => {
            CacheMgr.GetInstance().SetItem("test", "ffff");
        });
        _CreateTestBtn("������", () => {
            string str = CacheMgr.GetInstance().GetItem("test");
            Debug.Log(str);
        });
        _CreateTestBtn("��csv����", () => {
            var data = TableMgr.GetInstance().PlayerData;
            for(var i = 0; i < data.Count; i++)
            {
                var d = data[i];
                Debug.Log("����: "+d.Name+"__" +d.Age + "__" + d.Score; // Debug.Log(f.Name);
            }
        });
        //��˿ //�����
        //�¼����� //�����
        //���� //�����
        //��Ч //�����
        //���ػ��� //�����
        //��ֵ���� //�����
        //�б�
        //����

    }

}
