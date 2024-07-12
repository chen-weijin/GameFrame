using framework;
using System;
using UnityEngine.UI;

public class PopupNormal : PopupBase
{
    public const string Prefab_Path = "prefabs/panel_normal";

    //public static PopupNormal Create()
    //{
    //    var mo = UIManager.CreateLayer<PopupNormal>();
    //    PopupMgr.Instance.Push(mo);
    //    mo.CloseType = 0;
    //    mo.HideType = 0;
    //    return mo;
    //}

    protected override void Start()
    {
        base.Start();
        gameObject.FindInChildren("btn_close").GetComponent<Button>().onClick.AddListener(_Close);
        gameObject.FindInChildren("btn_cancel").GetComponent<Button>().onClick.AddListener(_Close);
        gameObject.FindInChildren("btn_submit").GetComponent<Button>().onClick.AddListener(_Close);
    }
    public void SetTitle(string title)
    {
        gameObject.FindInChildren("txt_title").GetComponent<Text>().text = title;
    }
    public void SetContent(string content)
    {
        gameObject.FindInChildren("txt_content").GetComponent<Text>().text = content;
    }
    public void SetSubmitHandle(Action act)
    {

    }
    public void SetCancelHandle(Action act)
    {

    }
}
