using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexAdsShower : MonoBehaviour
{
    public event Action OpenedAd;
    public event Action ClosedAd;
    public event Action Rewarded;

    [SerializeField] private int _showFullscreenLevelsPeriod;

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();

    [DllImport("__Internal")]
    private static extern void ShowRewardedAd();

    public void TryShowFullscreen(int level)
    {
        if (level % _showFullscreenLevelsPeriod == 0)
            ShowFullscreen();
    }

    public void ShowRewarded()
    {
        ShowRewardedAd();
    }

    private void ShowFullscreen()
    {
        ShowFullscreenAd();
    }

    public void OnCloseAd()
    {
        ClosedAd?.Invoke();
    }

    public void OnOpenAd()
    {
        OpenedAd?.Invoke();
    }

    public void OnRewarded()
    {
        Rewarded?.Invoke();
    }
}
