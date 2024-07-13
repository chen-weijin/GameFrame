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
        _CreateTestBtn("提示窗", () => {
            GameUIMgr.Instance.Tip();
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
        _CreateTestBtn("proto压缩数据", () => {
            //personInfo info = new personInfo();
            //info.Name = "ys";
            //info.Age = 50;
            //info.Money = 9999;
            //info.Phone.Add(new personInfo.Types.PhoneNumber { Number = "12123", Type = PhoneType.Home });
            //byte[] msg = ProtoBufffer.Serialize(info);
            //Debug.Log(msg.Length);

            //personInfo deinfo = ProtoBufffer.DeSerialize<personInfo>(msg);
            //Debug.Log("姓名：" + deinfo.Name);
            //Debug.Log("年龄：" + deinfo.Age);
            //Debug.Log("资产：" + deinfo.Money);
            //Debug.Log($"{deinfo.Phone[0].Type}的电话号：{deinfo.Phone[0].Number}");
            //MainMessage msg = new MainMessage();
            //msg.Type = MessageId.MessageTypeAId;
            //msg.MessageA = new MessageTypeA();
            //msg.MessageA.ContentA = "hahhaha";

        });
        _CreateTestBtn("proto解压数据", () => {
        });

        _CreateTestBtn("进入登陆界面", () => {
            SceneManager.LoadScene("Login");
        });

        //_CreateTestBtn("连接socket", () => {
        //    _socket = new NetSocket();
        //    _socket.InitSocket();
        //});

        _CreateTestBtn("发送protoA", () => {
            var data = new MessageTypeA();
            data.ContentA = "hahahahahah!!";
            MessageMgr.Instance.SendData(data);
        });
        _CreateTestBtn("发送protoV", () => {
            var data = new MessageTypeB();
            data.NumberB = 12345;
            MessageMgr.Instance.SendData(data);
        });




        //_CreateTestBtn("微信登陆", () => {
        //});
        //_CreateTestBtn("微信授权", () => {
        //});
        //_CreateTestBtn("获取用户信息", () => {
        //});

        //吐丝 //已完成
        //事件机制 //已完成
        //弹窗 //已完成
        //提示窗 //已完成
        //音效 //已完成
        //本地缓存 //已完成
        //数值配置 //已完成
        //纵向无线列表//已完成
        //微信登陆 //已完成
        //登陆界面 //已完成
        //protobuf  //已完成
        //websocket框架嵌入 //已完成
        //websocket重构 
        //网络
        //纵向网格无线列表
        //资源加载界面


    }

}
