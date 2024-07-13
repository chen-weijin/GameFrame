using framework;
using System;
using UnityEngine.UI;

public class PopupNormal : PopupBase
{
    public const string Prefab_Path = "prefabs/panel_normal";

    private Action _submitHandle;
    private Action _cancelHandle;
    protected override void Start()
    {
        base.Start();
        gameObject.FindInChildren("btn_close").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_cancelHandle != null)
            {
                _cancelHandle();
            }
            _cancelHandle();
            _Close();
        });
        gameObject.FindInChildren("btn_cancel").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_cancelHandle != null)
            {
                _cancelHandle();
            }
            _Close();
        });
        gameObject.FindInChildren("btn_submit").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_submitHandle != null)
            {
                _submitHandle();
            }
            _Close();
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
    public void SetSubmitHandle(Action act)
    {
        _submitHandle = act;
    }
    public void SetCancelHandle(Action act)
    {
        _cancelHandle = act;
    }
}
