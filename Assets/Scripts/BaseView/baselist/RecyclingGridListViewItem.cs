using UnityEngine;

/// <summary>
/// 列表item，你自己写的列表item需要继承该类
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class RecyclingGridListViewItem : MonoBehaviour
{

    private RecyclingGridListView parentList;

    /// <summary>
    /// 循环列表
    /// </summary>
    public RecyclingGridListView ParentList
    {
        get => parentList;
    }

    private int currentRow;
    /// <summary>
    /// 行号
    /// </summary>
    public int CurrentRow
    {
        get => currentRow;
    }

    private RectTransform rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// item更新事件响应函数
    /// </summary>
    public virtual void NotifyCurrentAssignment(RecyclingGridListView v, int row)
    {
        parentList = v;
        currentRow = row;
    }
}