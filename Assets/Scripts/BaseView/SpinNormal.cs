using UnityEngine.UI;

public class SpinNormal : BaseSpin
{

    public const string Prefab_Path = "prefabs/cm_toshi";

    public void SetContent(string str)
    {
        gameObject.FindInChildren("txt").GetComponent<Text>().text = str;
    }
}
