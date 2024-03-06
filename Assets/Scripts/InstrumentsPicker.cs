using UnityEngine;

public class InstrumentsPicker : MonoBehaviour
{
    [SerializeField] private ActiveInstrumentPresenter[] _presenters;

    private ActiveInstrumentPresenter _currentPresenter;

    private void OnEnable()
    {
        foreach (var presenter in _presenters)
            presenter.Click += TryChangeInstrument;
    }

    private void OnDisable()
    {
        foreach (var presenter in _presenters)
            presenter.Click -= TryChangeInstrument;
    }

    private void TryChangeInstrument(ActiveInstrumentPresenter presenter)
    {
        if(_currentPresenter != null)
        {
            _currentPresenter.Disactivate();
            _currentPresenter.Instrument.SetActive(false);

        }
        _currentPresenter = presenter;
        presenter.Activate();
        _currentPresenter.Instrument.SetActive(true);
    }
}
