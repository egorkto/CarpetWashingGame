using System.Collections.Generic;
using UnityEngine;

public class InstrumentsProgressInitializer : MonoBehaviour
{
    [SerializeField] private InstrumentPresenter[] _presenters;
    [SerializeField] private InstrumentType[] _startInstruments;

    public List<InstrumentType> GetData()
    {
        var data = new List<InstrumentType>();
        foreach(var presenter in _presenters)
            if(presenter.Opened)
                data.Add(presenter.Type);
        return data;
    }

    public void SetData(List<InstrumentType> data)
    {
        CloseAll();
        foreach (var presenter in _presenters)
            if (data.Contains(presenter.Type))
                presenter.Open();
    }

    private void CloseAll()
    {
        foreach (var presenter in _presenters)
            presenter.Close();
    }

    public InstrumentType[] GetStartInstrumentsData()
    {
        return _startInstruments;
    }
} 
