using framework;
using UnityEngine;
using UnityEngine.UI;

public class BlackMask : MonoBehaviour
{
    public const string Prefab_Path = "prefabs/bg_hei";
    public const string Prefab_Name = "panel_black";

    public static BlackMask Create()
    {
        var mo = UIManager.CreateLayer<BlackMask>();

        return mo;
    }
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            PopupMgr.Instance.PullByMask();
        });
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
