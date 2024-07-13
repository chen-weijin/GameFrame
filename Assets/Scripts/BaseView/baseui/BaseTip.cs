using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseTip : MonoBehaviour
{

    protected virtual void Start()
    {
        if(gameObject.FindInChildren("win") == null || gameObject.FindInChildren("bg") == null)
        {
            Debug.LogError("win,bg为节点必填项请确认!");
            return;
        }
        var win = gameObject.FindInChildren("win").gameObject;
        var bg = gameObject.FindInChildren("bg").gameObject;

        Vector2 pos = Input.mousePosition;
        var localPos = gameObject.transform.InverseTransformPoint(pos);
        _SetWinPosition(localPos, win);

        bg.GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }
    protected virtual void _SetWinPosition(Vector2 localPos, GameObject win)
    {
        var sizeWin = win.GetComponent<RectTransform>().GetContentSize() + new Vector2(10, 10);
        var sizeScreen = new Vector2(Screen.width, Screen.height);
        var sizeLock = sizeScreen - sizeWin;
        var posX = Math.Max(localPos.x, -sizeLock.x / 2);
        posX = Math.Min(localPos.x, sizeLock.x / 2);
        var posY = Math.Max(localPos.y, -sizeLock.y / 2);
        posY = Math.Min(localPos.y, sizeLock.y / 2);

        win.GetComponent<RectTransform>().SetPosition(new Vector2(posX, posY));
    }

    //if (Input.GetMouseButtonUp(0))
    //{
    //Vector2 pos = Input.mousePosition;
    //if (Input.touches.Length != 0)
    //{
    //    pos = Input.touches[0].position;
    //    Debug.Log("touch::::::::");
    //}

    //var localPos = gameObject.transform.InverseTransformPoint(pos);
    //Debug.Log("screen w:" + Screen.width + ", h:" + Screen.height);
    //Debug.Log("click pos x:" + pos.x + ",pos y:" + pos.y);
    //Debug.Log("click localPos x:" + localPos.x + ",pos y:" + localPos.y);
    //GameUIMgr.Instance.Spin("click pos x:" + pos.x + ",pos y:" + pos.y);
    //_SetWinPosition(localPos);
    //}
}
