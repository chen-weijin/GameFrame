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

    // 保存键值对到文件  
    public void SetItem(string key, string value)
    {
        storage[key] = value;
        Save();
    }

    // 从文件中获取键值对  
    public string GetItem(string key)
    {
        if (storage.ContainsKey(key))
        {
            return storage[key];
        }
        return null;
    }

    // 移除键值对  
    public void RemoveItem(string key)
    {
        if (storage.ContainsKey(key))
        {
            storage.Remove(key);
            Save();
        }
    }

    // 清除所有键值对  
    public void Clear()
    {
        storage.Clear();
        Save();
    }

    // 将当前存储的键值对保存到文件  
    private void Save()
    {
        string jsonData = JsonConvert.SerializeObject(storage);
        File.WriteAllText(filePath, jsonData);
    }

    // 从文件中加载键值对到当前存储  
    private void Load()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            storage = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }
    }
}
