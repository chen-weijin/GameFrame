using System;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;

public class WebSocketMgr : MonoBehaviour
{
    #region 单例
    private const string rootName = "[WebSocketMgr]";
    private static WebSocketMgr _instance;

    public static WebSocketMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                // 这里通常会在游戏开始时自动创建单例，或者你可以手动创建  
                // 为了简化，这里我们假设已经有一个活跃的 GameObject 来挂载这个脚本  
                // 在实际应用中，你可能需要在 Awake 或 Start 方法中检查 _instance 是否为 null  
                GameObject obj = new GameObject("WebSocketMgr");
                _instance = obj.AddComponent<WebSocketMgr>();
                DontDestroyOnLoad(obj); // 确保这个 GameObject 在加载新场景时不会被销毁  
            }
            return _instance;
        }
    }
    private void Awake()
    {
        // 检查是否已经是单例，如果不是则销毁自己  
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 初始化 WebSocketClient  
        // 连接 WebSocket 服务器等...  
    }

    // WebSocket 的其他管理方法...  

    // 确保在不需要 WebSocket 连接时能够正确关闭和清理资源  
    private void OnDestroy()
    {
        if (_webSocketClient != null)
        {
            _webSocketClient.CloseAsync(); // 假设 WebSocketClient 有 Close 方法来关闭连接  
        }
    }
    #endregion

    public const uint EVENT_OPEN_WEBSECKET = 10000;
    public const uint EVENT_MESSAGE_WEBSECKET = 10001;
    public const uint EVENT_CLOSE_WEBSECKET = 10002;
    public const uint EVENT_ERROR_WEBSECKET = 10003;

    private WebSocket _webSocketClient;
    public string address = "ws://localhost:8765";
    private int sendCount;
    private int receiveCount;

    #region 事件
    private Dictionary<uint, Action<UnityEngine.Object>> _dictMsgHandlerType = new Dictionary<uint, Action<UnityEngine.Object>>();

    /// <summary>
    /// 添加消息处理类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recvMsgId"></param>
    /// <param name="cb"></param>
    /// <returns></returns>
    public void AddMsgHandler(uint recvMsgId, Action<UnityEngine.Object> cb)
    {
        _dictMsgHandlerType[recvMsgId] = cb;
    }
    public void RemoveMsgHandler(uint recvMsgId)
    {
        _dictMsgHandlerType.Remove(recvMsgId);
    }
    public void PoseMsg(uint recvMsgId, UnityEngine.Object ob)
    {
        _dictMsgHandlerType.TryGetValue(recvMsgId, out Action<UnityEngine.Object> cb);
        if(cb != null)
        {
            cb(ob);
        }
    }
    #endregion

    public void InitSocket()
    {
        if (GetSocketState() != WebSocketState.Closed && GetSocketState() != WebSocketState.Closing) return;

        _webSocketClient = new WebSocket(address);
        _webSocketClient.OnOpen += Socket_OnOpen;
        _webSocketClient.OnMessage += Socket_OnMessage;
        _webSocketClient.OnClose += Socket_OnClose;
        _webSocketClient.OnError += Socket_OnError;
        Debug.Log(string.Format("Connecting..."));
        _webSocketClient.ConnectAsync();
    }

    private void Socket_OnOpen(object sender, OpenEventArgs e)
    {
        Debug.Log(string.Format("Connected: {0}", address));
        PoseMsg(EVENT_OPEN_WEBSECKET, null);
    }

    private void Socket_OnMessage(object sender, MessageEventArgs e)
    {
        if (e.IsBinary)
        {
            Debug.Log(string.Format("Receive Bytes ({1}): {0}", e.Data, e.RawData.Length));
        }
        else if (e.IsText)
        {
            Debug.Log(string.Format("Receive: {0}", e.Data));
        }
        receiveCount += 1;
        PoseMsg(EVENT_MESSAGE_WEBSECKET, null);
    }

    private void Socket_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log(string.Format("Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
        PoseMsg(EVENT_CLOSE_WEBSECKET, null);
    }

    private void Socket_OnError(object sender, ErrorEventArgs e)
    {
        Debug.Log(string.Format("Error: {0}", e.Message));
        PoseMsg(EVENT_ERROR_WEBSECKET, null);
    }

    // 其他公共或受保护的方法，用于发送消息、接收消息等...  
    public WebSocketState GetSocketState()
    {
        WebSocketState state = _webSocketClient == null ? WebSocketState.Closed : _webSocketClient.ReadyState;
        return state;
    }
    public void SendData(string sendText)
    {
        if (GetSocketState() != WebSocketState.Open) return;
        _webSocketClient.SendAsync(sendText);
        Debug.Log(string.Format("Send: {0}", sendText));
        sendCount += 1;
    }
    public void SendData2(string sendText)
    {
        if (GetSocketState() != WebSocketState.Open) return;
        var bytes = System.Text.Encoding.UTF8.GetBytes(sendText);
        _webSocketClient.SendAsync(bytes);
        Debug.Log(string.Format("Send Bytes ({1}): {0}", sendText, bytes.Length));
        sendCount += 1;
    }
}