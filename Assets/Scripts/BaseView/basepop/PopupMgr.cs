using framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMgr : Singleton<PopupMgr>
{
    private List<PopupBase> _list = new List<PopupBase>();
    private int _popIndexStart = 100;
    private BlackMask _black;
    /// <summary>
    /// �򿪴���
    /// </summary>
    /// <param name="view"></param>
    public void Push(PopupBase view)
    {
        _list.Add(view);
        view.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(),false);

        _Sort();
    }
    /// <summary>
    /// �ر����һ������
    /// </summary>
    public void Pull(PopupBase popup = null)
    {
        var lastPop = _list[_list.Count - 1];
        if (popup != lastPop) return;
        _list.RemoveAt(_list.Count - 1);
        lastPop.DestroyByHide();
        UIManager.SetSortingOrder(lastPop, _popIndexStart);

        _Sort();
    }
    /// <summary>
    /// ������ֹر����һ������
    /// </summary>
    public void PullByMask()
    {
        if (_list[_list.Count - 1].CloseType == 0) return;
        Pull();
    }
    /// <summary>
    /// �ر����д���
    /// </summary>
    public void PullAll()
    {
        if (_black) _black.DestroyMe();
        _black = null;

        for (var i = 0; i < _list.Count; i++)
        {
            _list[i].DestroyFlash();
        }
        _list.Clear();
    }
    /// <summary>
    /// ��������
    /// </summary>
    private void _Sort()
    {
        //��������
        if(_list.Count == 0)
        {
            if(_black) _black.DestroyMe();
            _black = null;
        }
        else
        {
            if (_black == null)
            {
                _black = UIManager.CreateLayer<BlackMask>();
                _black.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(), false);
                UIManager.SetSortingOrder(_black, _popIndexStart + _list.Count - 1);
            }
        }
        //������
        for(var i = 0; i < _list.Count; i++)
        {
            var idx = _popIndexStart + i;
            if(i == _list.Count - 1)
            {
                idx++;
            }
            UIManager.SetSortingOrder(_list[i], idx);
            if(i < _list.Count - 1 && _list[i].HideType == 0)
            {
                _list[i].Hide();
            }
            else
            {
                _list[i].Show();
            }
        }
    }

}
