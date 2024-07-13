using UnityEngine;
using UnityWebSocket;

public class NetSocket
{
    public string address = "ws://localhost:8765";
    public string sendText = "Hello World!";
    public bool logMessage = true;

    private IWebSocket socket;

    private int sendCount;
    private int receiveCount;
    public void InitSocket()
    {

        socket = new WebSocket(address);
        socket.OnOpen += Socket_OnOpen;
        socket.OnMessage += Socket_OnMessage;
        socket.OnClose += Socket_OnClose;
        socket.OnError += Socket_OnError;
        Debug.Log(string.Format("Connecting..."));
        socket.ConnectAsync();
        sendCount = 0;
    }
    public void CloseSocket()
    {
        if(socket != null)
        {
            socket.CloseAsync();
        }
    }
    public void SendData(string data)
    {
        socket.SendAsync(sendText);
        Debug.Log(string.Format("Send: {0}", sendText));
        sendCount += 1;
    }
    public void SendData2(string data)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(sendText);
        socket.SendAsync(bytes);
        Debug.Log(string.Format("Send Bytes ({1}): {0}", sendText, bytes.Length));
        sendCount += 1;
    }
    private void Socket_OnOpen(object sender, OpenEventArgs e)
    {
        Debug.Log(string.Format("Connected: {0}", address));
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
    }

    private void Socket_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log(string.Format("Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
    }

    private void Socket_OnError(object sender, ErrorEventArgs e)
    {
        Debug.Log(string.Format("Error: {0}", e.Message));
    }
}
