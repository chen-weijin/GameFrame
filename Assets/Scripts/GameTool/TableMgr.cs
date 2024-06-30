using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string Name;
    public int Age;
    public int Score;
}
public class TableMgr
{
    private static TableMgr instance;

    private List<PlayerData> _playerData;
    public List<PlayerData> PlayerData { get => _playerData; }
    public TableMgr()
    {
        TextAsset testCsv = (TextAsset)Resources.Load("data/test");
        var testData = CSVReader.ReadCSV<PlayerData>(testCsv.text);
    }


    public static TableMgr GetInstance()
    {
        if (instance == null)
        {
            instance = new TableMgr();
        }
        return instance;
    }
}
