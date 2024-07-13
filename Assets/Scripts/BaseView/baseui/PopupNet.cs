using framework;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupNet : MonoBehaviour
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

    protected void Start()
    {
        gameObject.FindInChildren("btn_close").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_cancelHandle != null)
            {
                _cancelHandle.Invoke();
            }
        });
        gameObject.FindInChildren("btn_cancel").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_cancelHandle != null)
            {
                _cancelHandle.Invoke();
            }
        });
        gameObject.FindInChildren("btn_submit").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_submitHandle != null)
            {
                _submitHandle.Invoke();
            }
        });
    }
    public void SetTitle(string title)
    {
        gameObject.FindInChildren("txt_title").GetComponent<Text>().text = title;
    }
    public void SetContent(string content)
    {
        gameObject.FindInChildren("txt_content").GetComponent<Text>().text = content;
    }
    private Action _submitHandle;
    private Action _cancelHandle;
    public void SetSubmitHandle(Action act)
    {
        _submitHandle = act;
    }
    public void SetCancleHandle(Action act)
    {
        _cancelHandle = act;
    }
}
