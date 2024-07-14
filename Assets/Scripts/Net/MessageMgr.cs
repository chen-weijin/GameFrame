using framework;
using TestGoogleProtoBuff;

public class MessageMgr :Singleton<MessageMgr>
{
    protected override void Init()
    {
        base.Init();
        WebSocketMgr.Instance.AddLis<MessageTypeA>(MessageTypeA);
        WebSocketMgr.Instance.AddLis<MessageTypeB>(MessageTypeB);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        WebSocketMgr.Instance.RemoveLis<MessageTypeA>(MessageTypeA);
        WebSocketMgr.Instance.RemoveLis<MessageTypeB>(MessageTypeB);
    }
    private void MessageTypeA(MessageTypeA message)
    {
        //数据处理
    }
    private void MessageTypeB(MessageTypeB message)
    {
        //数据处理
    }
}
