using framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

public class GameUIMgr : Singleton<GameUIMgr>
{
    public PopupNormal PopNormal()
    {
        var mo = UIManager.CreateLayer<PopupNormal>();
        PopupMgr.Instance.Push(mo);
        mo.CloseType = 0;
        mo.HideType = 0;
        return mo;
    }
    public Spin Spin(string content)
    {
        var mo = UIManager.CreateObject<Spin>();
        mo.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(), false);
        mo.SetContent(content);
        mo.Show();
        return mo;
    }
    public void ViewTest()
    {
        var testView = UIManager.CreateEmptyLayer<ViewTest>();
        testView.SetParent(UIManager.GetRootCanvas(), false);
        UIManager.SetSortingOrder(testView, 10000);
    }
}
