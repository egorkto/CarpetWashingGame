using System;
using UnityEngine;

public class InstrumentsOpener : MonoBehaviour
{
    public event Action OpenedInstrument;

    [SerializeField] private InstrumentPresenter[] _presenters;
    [SerializeField] private YandexAdsShower _adsShower;
    [SerializeField] private GameObject _window;

    private InstrumentType _currentType = InstrumentType.None;

    private void OnEnable()
    {
        InstrumentPresenter.Click += OpenWindow;
        _adsShower.Rewarded += TryOpenCurrent;
    }

    private void OnDisable()
    {
        InstrumentPresenter.Click -= OpenWindow;
        _adsShower.Rewarded -= TryOpenCurrent;
    }

    private void OpenWindow(InstrumentType type)
    {
        _window.gameObject.SetActive(true);
        _currentType = type;
    }

    private void TryOpenCurrent()
    {
        foreach (var presenter in _presenters)
        {
            if (presenter.Type == _currentType)
            {
                presenter.Open();
                OpenedInstrument?.Invoke();
            }
        }
    }
}
