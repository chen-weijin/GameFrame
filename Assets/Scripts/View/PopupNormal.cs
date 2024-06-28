using framework;
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
}
