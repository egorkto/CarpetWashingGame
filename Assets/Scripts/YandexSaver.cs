using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSaver : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void SetScore(int score);

    public void Save(ProgressData data)
    {
        var json = JsonUtility.ToJson(data);
        SaveExtern(json);
        SetScore(data.Level);
    }
}
