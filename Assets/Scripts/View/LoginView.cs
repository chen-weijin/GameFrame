using framework;
using UnityEngine.UI;

public class LoginView : BaseView
{
    protected override void Start()
    {
        WebSocketMgr.Instance.SetHandle(new MessageHandle());
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