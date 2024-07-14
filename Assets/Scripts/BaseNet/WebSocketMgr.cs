using Google.Protobuf;
using System;
using System.Collections.Generic;
using TestGoogleProtoBuff;
using UnityEngine;
using UnityWebSocket;
using framework;

public class WebSocketMgr : SingletonMono<WebSocketMgr>
{

    private WebSocket _webSocketClient;
    public string address = "ws://localhost:8765";
    private int sendCount;
    private int receiveCount;

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
    private void OnDestroy()
    {
        if (_webSocketClient != null)
        {
            _webSocketClient.CloseAsync(); // 假设 WebSocketClient 有 Close 方法来关闭连接  
        }
    }

    public WebSocketState GetSocketState()
    {
        WebSocketState state = _webSocketClient == null ? WebSocketState.Closed : _webSocketClient.ReadyState;
        return state;
    }
    public void SendByte(byte[] data)
    {
        if (GetSocketState() != WebSocketState.Open) return;
        _webSocketClient.SendAsync(data);
        Debug.Log(string.Format("Send Bytes ({1}): {0}", data, data.Length));
        sendCount += 1;
    }

    #region socket handle
    public const uint EVENT_OPEN_WEBSECKET = 10000;
    public const uint EVENT_MESSAGE_WEBSECKET = 10001;
    public const uint EVENT_CLOSE_WEBSECKET = 10002;
    public const uint EVENT_ERROR_WEBSECKET = 10003;
    private Dictionary<uint, Action<EventArgs>> _dictMsgHandlerType = new Dictionary<uint, Action<EventArgs>>();

    /// <summary>
    /// 添加消息处理类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="recvMsgId"></param>
    /// <param name="cb"></param>
    /// <returns></returns>
    public void AddMsgHandler(uint recvMsgId, Action<EventArgs> cb)
    {
        _dictMsgHandlerType[recvMsgId] = cb;
    }
    public void RemoveMsgHandler(uint recvMsgId)
    {
        _dictMsgHandlerType.Remove(recvMsgId);
    }
    public void PostNetWork(uint netId, EventArgs ob)
    {
        _dictMsgHandlerType.TryGetValue(netId, out Action<EventArgs> cb);
        if (cb != null) cb(ob);
    }

    #endregion

    #region socket event
    private void Socket_OnOpen(object sender, OpenEventArgs e)
    {
        Debug.Log(string.Format("Connected: {0}", address));
        PostNetWork(EVENT_OPEN_WEBSECKET, null);
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
        PostNetWork(EVENT_MESSAGE_WEBSECKET, e);

        MainMessage mainMsg = ProtoBufffer.DeSerialize<MainMessage>(e.RawData);
        var id = _GetKey(mainMsg.Type);
        _handle.PostMsg(id,mainMsg);
    }

    private void Socket_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log(string.Format("Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
        PostNetWork(EVENT_CLOSE_WEBSECKET, e);
    }

    private void Socket_OnError(object sender, ErrorEventArgs e)
    {
        Debug.Log(string.Format("Error: {0}", e.Message));
        PostNetWork(EVENT_ERROR_WEBSECKET, e);
    }
    #endregion

    #region Message
    private IMessageHandle _handle;
    public void SetHandle(IMessageHandle handle)
    {
        _handle = handle;
    }

    private string _GetKey<T>() where T : IMessage
    {
        var id = _handle.GetMessageIdByType<T>();
        return _GetKey(id);
    }
    private string _GetKey(MessageId id)
    {
        return "MSG_" + id;
    }

    public void AddLis<T>(Action<T> act) where T : IMessage
    {
        Ntfy.Instance.Add(_GetKey<T>(), act);
    }
    public void RemoveLis<T>(Action<T> act) where T : IMessage
    {
        Ntfy.Instance.Remove(_GetKey<T>(), act);
    }

    public void SendData<T>(T t) where T : IMessage
    {
        MainMessage mainMsg = _handle.GetMainMsg(t);
        byte[] data = ProtoBufffer.Serialize(mainMsg);
        SendByte(data);
    }
    #endregion
}


public interface IMessageHandle
{
    public MessageId GetMessageIdByType<T>();
    public MainMessage GetMainMsg<T>(T t);
    public void PostMsg(string id, MainMessage mainMsg);
}

//public void SendData(string sendText)
//{
//    if (GetSocketState() != WebSocketState.Open) return;
//    _webSocketClient.SendAsync(sendText);
//    Debug.Log(string.Format("Send: {0}", sendText));
//    sendCount += 1;
//}
//public void SendData2(string sendText)
//{
//    if (GetSocketState() != WebSocketState.Open) return;
//    var bytes = System.Text.Encoding.UTF8.GetBytes(sendText);
//    _webSocketClient.SendAsync(bytes);
//    Debug.Log(string.Format("Send Bytes ({1}): {0}", sendText, bytes.Length));
//    sendCount += 1;
//}