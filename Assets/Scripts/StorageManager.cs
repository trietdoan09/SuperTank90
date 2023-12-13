using System;
using System.IO;
using UnityEngine;
public class StorageManager
{
    public static bool SaveToFile(string fileName, string json)
    {
        try
        {
            var fileStream = new FileStream(fileName, FileMode.Create);
            using var writer = new StreamWriter(fileStream);
            writer.Write(json);
            return true;
        }
        catch(Exception e)
        {
            Debug.Log("Write to file: " + e.Message);
        }
        return false;
    }

    public static string ReadFromFile(string fileName)
    {
        if(File.Exists(fileName))
        {
            using var reader = new StreamReader(fileName);
            string json = reader.ReadToEnd();
            return json;
        }
        else
        {
            Debug.LogWarning("File not found");
        }
        return "Success";
    }
}
