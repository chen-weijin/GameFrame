using System;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    //0:非激活状态下隐藏 1:任何时候都不隐藏
    protected int _hideType = 0;
    //0:点击蒙版不关闭 1:点击蒙版关闭
    protected int _closeType = 0;

    private bool _isShow = false;
    private bool _isClose = false;
    private CCAction _shAct;
    public int HideType { get => _hideType; set => _hideType = value; }
    public int CloseType { get => _closeType; set => _closeType = value; }
    protected virtual void Start()
    {
    }
    
    public void Show()
    {
        if (_isShow == true) return;
        _isShow = true;

        _ShowAni();
    }
    public void Hide()
    {
        if (_isShow == false) return;
        _isShow = false;

        _HideAni();
    }
    public void DestroyFlash()
    {
        Destroy(gameObject);
    }
    public void DestroyByHide()
    {
        if (_isClose == true) return;
        _isClose = true;

        Destroy(gameObject);
        //HideAni(() =>
        //{
        //    Destroy(gameObject);
        //});
    }


    protected void _Close()
    {
        PopupMgr.Instance.Pull(this);
    }
    /// <summary>
    /// 显示动画
    /// </summary>
    /// <param name="endAct"></param>
    protected virtual void _ShowAni(Action endAct = null)
    {
        gameObject.SetActive(true);
        gameObject.GetRectTransform().SetScale(0);
        _shAct?.Stop();
        _shAct = gameObject.GetRectTransform().BeginAction()
            .ScaleTo(0.2f, 1, DG.Tweening.Ease.OutBack)
            .Run();
    }
    /// <summary>
    /// 隐藏动画
    /// </summary>
    /// <param name="endAct"></param>
    protected virtual void _HideAni(Action endAct = null)
    {

        gameObject.SetActive(true);
        gameObject.GetRectTransform().SetScale(1);
        _shAct?.Stop();
        _shAct = gameObject.GetRectTransform().BeginAction()
            .ScaleTo(0.2f, 0, DG.Tweening.Ease.OutBack)
            .CallFunc(() =>
            {
                gameObject.SetActive(false);
                if (endAct != null) endAct();
            })
            .Run();
    }
}
