using System.Collections;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        PopupMgr.Instance.PullAll();
    }
}