
using System.Collections.Generic;
using TestGoogleProtoBuff;
using WLMain;

public class MessageHandle : IMessageHandle
{
    private Dictionary<object, MessageId> _dicClass2Id = new Dictionary<object, MessageId>() {
        {typeof(MessageTypeA),MessageId.MessageTypeAId  },
        {typeof(MessageTypeB),MessageId.MessageTypeBId  }
    };
    public MessageId GetMessageIdByType<T>()
    {
        _dicClass2Id.TryGetValue(typeof(T), out MessageId mid);
        return mid;
    }
    public void PostMsg(string id, MainMessage mainMsg)
    {

        switch (mainMsg.Type)
        {
            case MessageId.MessageTypeAId:
                Ntfy.Instance.Post(id, mainMsg.MessageA);
                break;
            case MessageId.MessageTypeBId:
                Ntfy.Instance.Post(id, mainMsg.MessageB);
                break;
        }
    }


    public MainMessage GetMainMsg<T>(T t)
    {
        MainMessage mainMsg = new MainMessage();
        mainMsg.Type = GetMessageIdByType<T>();

        switch (mainMsg.Type)
        {
            case MessageId.MessageTypeAId:
                mainMsg.MessageA = t as MessageTypeA;
                break;
            case MessageId.MessageTypeBId:
                mainMsg.MessageB = t as MessageTypeB;
                break;
        }
        return mainMsg;
    }

}