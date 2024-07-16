using framework;
using System;
using System.Collections;
using TestGoogleProtoBuff;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityWebSocket;
using WeChatWASM;

public class BaseView : MonoBehaviour
{
    // Use this for initialization
    protected virtual void Start()
    {
        _InitText();
        WebSocketMgr.Instance.AddMsgHandler(WebSocketMgr.EVENT_OPEN_WEBSECKET, _OpenSocket);
        WebSocketMgr.Instance.AddMsgHandler(WebSocketMgr.EVENT_CLOSE_WEBSECKET, _CloseSocket);
        WebSocketMgr.Instance.AddMsgHandler(WebSocketMgr.EVENT_ERROR_WEBSECKET, _ErrorSocket);
        WebSocketMgr.Instance.AddLis<MessageTypeA>(MessageAHandle);
        WebSocketMgr.Instance.AddLis<MessageTypeB>(MessageBHandle);
    }
    private void MessageAHandle(MessageTypeA msg)
    {
        Debug.Log("MessageAHandle:" + msg.ContentA);
    }
    private void MessageBHandle(MessageTypeB msg)
    {
        Debug.Log("MessageBHandle:" + msg.NumberB);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        PopupMgr.Instance.PullAll();
        WebSocketMgr.Instance.RemoveMsgHandler(WebSocketMgr.EVENT_OPEN_WEBSECKET);
        WebSocketMgr.Instance.RemoveMsgHandler(WebSocketMgr.EVENT_CLOSE_WEBSECKET);
        WebSocketMgr.Instance.RemoveMsgHandler(WebSocketMgr.EVENT_ERROR_WEBSECKET);
        WebSocketMgr.Instance.RemoveLis<MessageTypeA>(MessageAHandle);
        WebSocketMgr.Instance.RemoveLis<MessageTypeB>(MessageBHandle);
    }

    protected void Login()
    {
        WebSocketMgr.Instance.InitSocket();
    }
    protected void _OpenSocket(EventArgs ob)
    {
        //进入大厅
        SceneManager.LoadScene("Lobby");
    }
    protected void _MessageSocket(EventArgs ob)
    {

    }
    protected void _CloseSocket(EventArgs ob)
    {
        //弹出断网窗口,回到登陆界面
        _NetPop();
    }
    protected void _ErrorSocket(EventArgs ob)
    {
        //弹出断网窗口,回到登陆界面
        _NetPop();
    }

    private PopupNet _popNet;
    private void _NetPop()
    {
        if (_popNet != null) return;

        _popNet = UIManager.CreateLayer<PopupNet>();
        _popNet.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(), false);
        UIManager.SetSortingOrder(_popNet, 400);
        _popNet.SetTitle("网络错误");
        _popNet.SetContent("网络错误");
        _popNet.SetSubmitHandle(() => {
            WebSocketMgr.Instance.InitSocket();
            Destroy(_popNet.gameObject);
            _popNet = null;
        });
        _popNet.SetCancleHandle(() => {
            if (this.GetType() != typeof(LoginView))
            {
                SceneManager.LoadScene("Login");
            }
            Destroy(_popNet.gameObject);
            _popNet = null;
        });
    }
    protected void _InitText()
    {
        WX.GetWXFont("https://7072-prod-2gneo2k5fa9692f8-1327522290.tcb.qcloud.la/webgl/font.otf", (font) =>
        {

            // 遍历场景中的所有物体
            foreach (var obj in FindObjectsOfType<GameObject>())
            {
                // 获取每个物体上的 Text 组件
                Text textComponent = obj.GetComponent<Text>();
                if (textComponent != null)
                {
                    // 在这里对获取到的 Text 组件进行操作
                    Debug.Log("找到 Text 组件: " + textComponent.name);
                    textComponent.font = font;
                }
            }
        });
    }
}