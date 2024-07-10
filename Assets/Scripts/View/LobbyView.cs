using framework;
using System.Collections;
using UnityEngine;

public class LobbyView : BaseView
{
    private string[] randomTitles = new[] {
            "黄沙百战穿金甲，不破楼兰终不还",
            "且将新火试新茶，诗酒趁年华",
            "苟利国家生死以，岂因祸福避趋之",
            "枫叶经霜艳，梅花透雪香",
            "夏虫不可语于冰",
            "落花无言，人淡如菊",
            "宠辱不惊，闲看庭前花开花落；去留无意，漫随天外云卷云舒",
            "衣带渐宽终不悔，为伊消得人憔悴",
            "从善如登，从恶如崩",
            "欲穷千里目，更上一层楼",
            "草木本无意，荣枯自有时",
            "纸上得来终觉浅，绝知此事要躬行",
            "不是一番梅彻骨，怎得梅花扑鼻香",
            "青青子衿，悠悠我心",
            "瓜田不纳履，李下不正冠"
        };
    void Start()
    {
        GameUIMgr.Instance.ViewTest();
        _InitList();
    }
    private void _InitList()
    {
        var scv = UIManager.GetRootCanvas().gameObject.FindInChildren("sv").GetComponent<RecyclingListView>();
        scv.ItemCallback = PopulateItem;
        scv.RowCount = randomTitles.Length;
    }
    /// <summary>
    /// item更新回调
    /// </summary>
    /// <param name="item">复用的item对象</param>
    /// <param name="rowIndex">行号</param>
    private void PopulateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as TestChildItem;
        child.ChildData = new TestChildData(randomTitles[rowIndex], rowIndex);
    }
}