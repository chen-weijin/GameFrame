using framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPopManager : Singleton<ViewPopManager>
{
    private List<ViewPop> _list = new List<ViewPop>();
    private int _popIndexStart = 100;
    private BlackMask _black;
    /// <summary>
    /// �򿪴���
    /// </summary>
    /// <param name="view"></param>
    public void Push(ViewPop view)
    {
        _list.Add(view);
        view.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(),false);
        view.EnsureComponent<Canvas>();
        view.EnsureComponent<GraphicRaycaster>();

        _Sort();
    }
    /// <summary>
    /// �ر����һ������
    /// </summary>
    public void Pull()
    {
        var lastPop = _list[_list.Count - 1];
        _list.RemoveAt(_list.Count - 1);
        lastPop._DestroyByHide();
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
        _DelBlack();

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
        if(_list.Count == 0)
        {
            _DelBlack();
        }
        else
        {
            _AddBlack();
            UIManager.SetSortingOrder(_black, _popIndexStart + _list.Count - 1);
        }

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
    /// <summary>
    /// ���ִ���
    /// </summary>
    private void _AddBlack()
    {
        if (_black != null) return;
        _black = BlackMask.Create();
        _black.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(), false);
        _black.EnsureComponent<Canvas>();
        _black.EnsureComponent<GraphicRaycaster>();
    }

    /// <summary>
    /// ���ִ���
    /// </summary>
    private void _DelBlack()
    {
        _black.DestroyMe();
        _black = null;
    }

}
