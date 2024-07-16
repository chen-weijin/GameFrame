using framework;
using UnityEngine;
using UnityEngine.UI;
using WeChatWASM;

public class LoginView : BaseView
{
    protected override void Start()
    {
        //控制器初始化,只需要执行一次就行
        WebSocketMgr.Instance.SetHandle(new MessageHandle());
        _ = MessageMgr.Instance;
        _ = GameDataMgr.Instance;
        ////////
        base.Start();
        UIManager.GetRootCanvas().gameObject.FindInChildren("btn_start").GetComponent<Button>().onClick.AddListener(() =>
        {
            //WXPlatform.Login((code) =>
            //{
            //    SceneManager.LoadScene("Lobby");
            //});
            //SceneManager.LoadScene("Lobby"); 
            WebSocketMgr.Instance.InitSocket();
        });
    }

}