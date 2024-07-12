using framework;
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
        UIManager.SetSortingOrder(mo, 300);
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
    public TipWin Tip()
    {
        var mo = UIManager.CreateObject<TipWin>();
        mo.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(), false);
        UIManager.SetSortingOrder(mo, 200);
        return mo;
    }
    //public void NetClose()
    //{
    //    var mo = UIManager.CreateLayer<PopupNet>();
    //    mo.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(), false);
    //    UIManager.SetSortingOrder(mo, 400);
    //    mo.SetTitle("ÍøÂç´íÎó");
    //    mo.SetContent("ÍøÂç´íÎó");
    //    mo.SetSubmitHandle(() =>{ 
            
    //    });
    //    mo.SetCancleHandle(() => {
            
    //    });

    //}
}
