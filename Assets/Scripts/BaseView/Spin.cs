using framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spin : MonoBehaviour
{

    public const string Prefab_Path = "prefabs/cm_toshi";

    //public static Spin Create(string content)
    //{
    //    var mo = UIManager.CreateObject<Spin>();
    //    mo.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas(),false);
    //    mo.SetContent(content);
    //    mo.Show();
    //    return mo;
    //}
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
