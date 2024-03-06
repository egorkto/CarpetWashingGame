using System.IO;
using UnityEngine;

public class ProgressSaver
{
    private const string fileName = "save.json";

    public void Save(ProgressData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, json);
    }
}
