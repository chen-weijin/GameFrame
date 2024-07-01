using framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class CacheMgr: UnityLocalStorage
{
    private static CacheMgr instance;

    public CacheMgr(string fileName) : base(fileName)
    {
    }

    public static CacheMgr GetInstance()
    {
        if (instance == null)
        {
            instance = new CacheMgr("localStorage.json");
        }
        return instance;

    }
}

  
public class UnityLocalStorage
{
    private string filePath;
    private Dictionary<string, string> storage = new Dictionary<string, string>();

    public UnityLocalStorage(string fileName)
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Load();
    }

    // �����ֵ�Ե��ļ�  
    public void SetItem(string key, string value)
    {
        storage[key] = value;
        Save();
    }

    // ���ļ��л�ȡ��ֵ��  
    public string GetItem(string key)
    {
        if (storage.ContainsKey(key))
        {
            return storage[key];
        }
        return null;
    }

    // �Ƴ���ֵ��  
    public void RemoveItem(string key)
    {
        if (storage.ContainsKey(key))
        {
            storage.Remove(key);
            Save();
        }
    }

    // ������м�ֵ��  
    public void Clear()
    {
        storage.Clear();
        Save();
    }

    // ����ǰ�洢�ļ�ֵ�Ա��浽�ļ�  
    private void Save()
    {
        string jsonData = JsonConvert.SerializeObject(storage);
        File.WriteAllText(filePath, jsonData);
    }

    // ���ļ��м��ؼ�ֵ�Ե���ǰ�洢  
    private void Load()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            storage = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }
    }
}
