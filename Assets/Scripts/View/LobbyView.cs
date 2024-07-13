using framework;
using System.Collections;
using UnityEngine;

public class LobbyView : BaseView
{
    protected override void Start()
    {
        base.Start();
        GameUIMgr.Instance.ViewTest();
    }
}