using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexLoader : MonoBehaviour
{
    public event Action<ProgressData> RecievedData;

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public void Load()
    {
        LoadExtern();
    }

    public void SetData(string json)
    {
        RecievedData?.Invoke(JsonUtility.FromJson<ProgressData>(json));
    }
}
