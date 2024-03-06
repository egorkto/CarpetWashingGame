using UnityEngine;

public class AudioTuner : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundSource;
    [SerializeField] private AudioSource _instrumentsEffectsSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private YandexAdsShower _adsShower;

    private bool _adOpen = false;

    public void SetBackgroundMuted(bool muted)
    {
        _backgroundSource.mute = muted;
    }

    public void SetEffectsMuted(bool muted)
    {
        _instrumentsEffectsSource.mute = muted;
        _sfxSource.mute = muted;
    }

    private void OnEnable()
    {
        _adsShower.OpenedAd += OnAdOpen;
        _adsShower.ClosedAd += OnAdClose;
    }

    private void OnDisable()
    {
        _adsShower.OpenedAd -= OnAdOpen;
        _adsShower.ClosedAd -= OnAdClose;
    }

    private void OnAdOpen()
    {
        _adOpen = true;
        AudioListener.volume = 0f;
    }

    private void OnAdClose()
    {
        _adOpen = false;
        AudioListener.volume = 1f;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !_adOpen)
            AudioListener.volume = 1f;
        else
            AudioListener.volume = 0f;
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused || _adOpen)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }
}
