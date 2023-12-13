using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageHelper
{
    public GameData data;
    private readonly string fileName = "GameData.txt";

    public void LoadData()
    {
        data = new GameData();
        //đọc chuỗi string từ file
        string json = StorageManager.ReadFromFile(fileName);
        if (json.Equals("Success")) return;
        //chuyển từ string sang object c#
        JsonUtility.FromJsonOverwrite(json, data);
    }

    public void SaveData()
    {
        //chuyển từ object c# sang string
        string json = JsonUtility.ToJson(data);
        StorageManager.SaveToFile(fileName, json);
    }

    //dành cho list
    public ListGameData list;
    private readonly string fileListName = "ListGameData.txt";
    public void LoadListData()
    {
        list = new ListGameData()
        {
            list = new List<GameData>()
        };
        //đọc chuỗi string từ file
        string json = StorageManager.ReadFromFile(fileListName);
        if (json.Equals("Success")) return;
        //chuyển từ string sang object c#
        list = JsonUtility.FromJson<ListGameData>(json);
    }

    public void SaveListData()
    {
        //chuyển từ object c# sang string
        string json = JsonUtility.ToJson(list);
        StorageManager.SaveToFile(fileListName, json);
    }
}
