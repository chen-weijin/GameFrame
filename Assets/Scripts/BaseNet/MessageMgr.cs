using framework;
using System;
using System.Collections.Generic;
using TestGoogleProtoBuff;
using UnityWebSocket;
using WLMain;

public class MessageMgr : Singleton<MessageMgr>
{
    private Dictionary< object, MessageId> _dicClass2Id = new Dictionary< object, MessageId>() {
        {typeof(MessageTypeA),MessageId.MessageTypeAId  },
        {typeof(MessageTypeB),MessageId.MessageTypeBId  }
    };

    private byte[] _GendData<T>(T t)
    {
        _dicClass2Id.TryGetValue(typeof(T), out MessageId value);

        MainMessage mainMsg = new MainMessage();
        mainMsg.Type = value;

        switch (mainMsg.Type)
        {
            case MessageId.MessageTypeAId:
                mainMsg.MessageA = t as MessageTypeA;
                break;
            case MessageId.MessageTypeBId:
                mainMsg.MessageB = t as MessageTypeB;
                break;
        }
        byte[] msg = ProtoBufffer.Serialize(mainMsg);
        return msg;
    }
    public void PostMsg(MessageEventArgs e)
    {
        MainMessage mainMsg = ProtoBufffer.DeSerialize<MainMessage>(e.RawData);

        switch (mainMsg.Type)
        {
            case MessageId.MessageTypeAId:
                Ntfy.Instance.Post(_GetKey(mainMsg.Type), mainMsg.MessageA);
                break;
            case MessageId.MessageTypeBId:
                Ntfy.Instance.Post(_GetKey(mainMsg.Type), mainMsg.MessageB);
                break;
        }

    }
    private string _GetKey(MessageId id)
    {
        return "MSG_" + id;
    }

    public void AddLis<T>(Action<T> act)
    {
        _dicClass2Id.TryGetValue(typeof(T), out MessageId value);

        Ntfy.Instance.Add(_GetKey(value), act);
    }
    public void RemoveLis<T>(Action<T> act)
    {
        _dicClass2Id.TryGetValue(typeof(T), out MessageId value);

        Ntfy.Instance.Remove(_GetKey(value), act);
    }

    public void SendData<T>(T t)
    {
        var data = _GendData(t);
        WebSocketMgr.Instance.SendByte(data);
    }
}
