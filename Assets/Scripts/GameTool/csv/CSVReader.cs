using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class CSVReader
{
    public static List<T> ReadCSV<T>(string csvContent) where T : new()
    {
        List<T> list = new List<T>();
        string[] lines = csvContent.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);


        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i] == "") break;
            string[] values = lines[i].Split(',');

            // 解析数据并创建PlayerData对象  
            T playerData = new();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            int index = 0;
            foreach (FieldInfo f in fields)
            {
                var tp = f.FieldType;
                var t1 = typeof(int);
                var t2 = typeof(string);
                var t3 = typeof(float);
                if (f.FieldType == typeof(int))
                {
                    f.SetValue(playerData, int.Parse(values[index]));
                }
                else if (f.FieldType == typeof(string))
                {
                    f.SetValue(playerData, values[index]);
                }
                else if (f.FieldType == typeof(float))
                {
                    f.SetValue(playerData, float.Parse(values[index]));
                }
                index++;
            }
            list.Add(playerData);
        }
        return list;

    }

}