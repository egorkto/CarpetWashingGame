using System.IO;
using UnityEngine;

public class ProgressLoader
{
    private const string fileName = "save.json";

    public bool TryLoad(out ProgressData progressData)
    {
        if(File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            var json = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
            var data = JsonUtility.FromJson<ProgressData>(json);
            progressData = data;
            return true;
        }
        progressData = new ProgressData();
        return false;
    }
}
