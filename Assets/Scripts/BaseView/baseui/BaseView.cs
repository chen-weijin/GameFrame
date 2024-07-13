using framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseView : MonoBehaviour
{
    // Use this for initialization
    protected virtual void Start()
    {
        WebSocketMgr.Instance.AddMsgHandler(WebSocketMgr.EVENT_OPEN_WEBSECKET, _OpenSocket);
        WebSocketMgr.Instance.AddMsgHandler(WebSocketMgr.EVENT_CLOSE_WEBSECKET, _CloseSocket);
        WebSocketMgr.Instance.AddMsgHandler(WebSocketMgr.EVENT_ERROR_WEBSECKET, _ErrorSocket);
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
    }

    protected void Login()
    {
        WebSocketMgr.Instance.InitSocket();
    }
    protected void _OpenSocket(Object ob)
    {
        //进入大厅
        SceneManager.LoadScene("Lobby");
    }
    protected void _MessageSocket(Object ob)
    {

    }
    protected void _CloseSocket(Object ob)
    {
        //弹出断网窗口,回到登陆界面
        _NetPop();
    }
    protected void _ErrorSocket(Object ob)
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
}