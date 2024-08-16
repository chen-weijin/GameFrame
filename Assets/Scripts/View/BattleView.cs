using framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WeChatWASM;

public class BattleView : BaseView
{
    protected override void Start()
    {
        var g = GetGrid();
        var person = g.gameObject.FindInChildren("Person");
    }

    public Transform GetGrid()
    {
        var s = SceneManager.GetActiveScene();
        var gArr = s.GetRootGameObjects();
        for (var i = 0; i < gArr.Length; i++)
        {
            if (gArr[i].name == "Grid")
            {
                return gArr[i].transform;
            }
        }
        return null;
    }

}