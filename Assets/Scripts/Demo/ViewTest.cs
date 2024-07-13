using framework;
using game;
using System;
using TestGoogleProtoBuff;
using UnityEngine;
using UnityEngine.SceneManagement;
using WeChatWASM;
using WLMain;

public class ViewTest : MonoBehaviour
{

    private int _index = 0;
    private NetSocket _socket;
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
        _CreateTestBtn("��ʾ��", () => {
            GameUIMgr.Instance.Tip();
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
                Debug.Log("����: "+d.Name+"__" +d.Age + "__" + d.Score); // Debug.Log(f.Name);
            }
        });
        _CreateTestBtn("protoѹ������", () => {
            //personInfo info = new personInfo();
            //info.Name = "ys";
            //info.Age = 50;
            //info.Money = 9999;
            //info.Phone.Add(new personInfo.Types.PhoneNumber { Number = "12123", Type = PhoneType.Home });
            //byte[] msg = ProtoBufffer.Serialize(info);
            //Debug.Log(msg.Length);

            //personInfo deinfo = ProtoBufffer.DeSerialize<personInfo>(msg);
            //Debug.Log("������" + deinfo.Name);
            //Debug.Log("���䣺" + deinfo.Age);
            //Debug.Log("�ʲ���" + deinfo.Money);
            //Debug.Log($"{deinfo.Phone[0].Type}�ĵ绰�ţ�{deinfo.Phone[0].Number}");
            //MainMessage msg = new MainMessage();
            //msg.Type = MessageId.MessageTypeAId;
            //msg.MessageA = new MessageTypeA();
            //msg.MessageA.ContentA = "hahhaha";

        });
        _CreateTestBtn("proto��ѹ����", () => {
        });

        _CreateTestBtn("�����½����", () => {
            SceneManager.LoadScene("Login");
        });

        //_CreateTestBtn("����socket", () => {
        //    _socket = new NetSocket();
        //    _socket.InitSocket();
        //});

        _CreateTestBtn("����protoA", () => {
            var data = new MessageTypeA();
            data.ContentA = "hahahahahah!!";
            MessageMgr.Instance.SendData(data);
        });
        _CreateTestBtn("����protoV", () => {
            var data = new MessageTypeB();
            data.NumberB = 12345;
            MessageMgr.Instance.SendData(data);
        });




        //_CreateTestBtn("΢�ŵ�½", () => {
        //});
        //_CreateTestBtn("΢����Ȩ", () => {
        //});
        //_CreateTestBtn("��ȡ�û���Ϣ", () => {
        //});

        //��˿ //�����
        //�¼����� //�����
        //���� //�����
        //��ʾ�� //�����
        //��Ч //�����
        //���ػ��� //�����
        //��ֵ���� //�����
        //���������б�//�����
        //΢�ŵ�½ //�����
        //��½���� //�����
        //protobuf  //�����
        //websocket���Ƕ�� //�����
        //websocket�ع� 
        //����
        //�������������б�
        //��Դ���ؽ���


    }

}
