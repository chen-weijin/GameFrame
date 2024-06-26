using framework;
using game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var btn = UIManager.CreateObject<XTestButton>();
        btn.GetComponent<RectTransform>().SetParent(UIManager.GetRootCanvas());
    }

}
