using framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewToshi : MonoBehaviour
{

    public const string Prefab_Path = "prefabs/cm_toshi";
    public const string Prefab_Name = "Toshi";

    public static ViewToshi Create(string content)
    {
        var mo = UIManager.CreateObject<ViewToshi>();
        mo.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(),false);
        mo.SetContent(content);
        mo.Show();
        return mo;
    }
    public void SetContent(string str)
    {
        gameObject.FindInChildren("txt").GetComponent<Text>().text = str;
    }
    public void Show()
    {
        gameObject.GetRectTransform().BeginAction()
            .DelayTime(3)
            .CallFunc(() => {
                Destroy(gameObject);
            })
            .Run();
    }
}
