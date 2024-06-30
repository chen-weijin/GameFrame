using framework;
using game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using WLMain;

public class Abc
{
    public string a = "aaa";
    public Dictionary<string, string> storage = new Dictionary<string, string>() { {"test","te" }};
}
public class Main : MonoBehaviour
{
    void Start()
    {
        GameUIMgr.Instance.ViewTest();
    }

}
