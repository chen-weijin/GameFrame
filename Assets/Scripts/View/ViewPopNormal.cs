using framework;
using UnityEngine.UI;

public class ViewPopNormal : ViewPop
{
    public const string Prefab_Path = "prefabs/panel_normal";
    public const string Prefab_Name = "panel_normal";

    public static ViewPopNormal Create()
    {
        var mo = UIManager.CreateLayer<ViewPopNormal>();
        ViewPopManager.Instance.Push(mo);
        mo.CloseType = 0;
        mo.HideType = 0;
        return mo;
    }

    protected override void Start()
    {
        base.Start();
        gameObject.FindInChildren("btn_close").GetComponent<Button>().onClick.AddListener(_Close);
        gameObject.FindInChildren("btn_cancel").GetComponent<Button>().onClick.AddListener(_Close);
        gameObject.FindInChildren("btn_submit").GetComponent<Button>().onClick.AddListener(_Close);
    }
}
