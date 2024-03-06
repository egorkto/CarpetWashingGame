using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private CarpetCreater _carpetCreator;
    [SerializeField] private InstrumentsOpener _opener;
    [SerializeField] private InstrumentsInitializer _instrumentsInitializer;
    [SerializeField] private CleaningProgressCalculator _progressCalculator;
    [SerializeField] private InstrumentsProgressInitializer _instrumentsProgressInitializer;
    [SerializeField] private LevelResultsPresenter _levelResultsPresenter;
    [SerializeField] private LevelProgressPresenter _levelProgressPresenter;
    [SerializeField] private YandexAdsShower _yandexAdsShower;
    [SerializeField] private YandexLoader _yandexLoader;
    [SerializeField] private YandexSaver _yandexSaver;
    [SerializeField] private GameObject _howToPlayWindow;

    private Carpet _carpet;
    private int _level;

    private void OnEnable()
    {
        _yandexLoader.RecievedData += LoadData;
        _opener.OpenedInstrument += SaveCurrent;
    }

    private void OnDisable()
    {
        _yandexLoader.RecievedData -= LoadData;
        _opener.OpenedInstrument -= SaveCurrent;
    }

    private void LoadData(ProgressData data)
    {
        var progress = data;
        if (progress.OpenedInstruments == null || progress.OpenedInstruments.Count == 0)
            progress = GetNewProgress();
        if (progress.Level == 1)
            _howToPlayWindow.gameObject.SetActive(true);
        _level = progress.Level;
        _carpet = _carpetCreator.CreateCarpet(progress.OpenedInstruments);
        _instrumentsInitializer.Initialize(_carpet);
        _progressCalculator.Initialize(_carpet);
        _levelResultsPresenter.SetDirty(_carpet.GetData());
        _instrumentsProgressInitializer.SetData(progress.OpenedInstruments);
        _levelProgressPresenter.Present(progress.Level);
        _yandexAdsShower.TryShowFullscreen(progress.Level);
    }

    private ProgressData GetNewProgress()
    {
        var progress = new ProgressData();
        progress.Level = 1;
        progress.OpenedInstruments = _instrumentsProgressInitializer.GetStartInstrumentsData().ToList();
        return progress;
    }

    private void Start()
    {
        _yandexLoader.Load();
        //var data = new ProgressData();
        //data.OpenedInstruments = new List<InstrumentType>();
        //LoadData(data);
    }

    public void SaveCurrent()
    {
        _yandexSaver.Save(GetCurrentData());
    }

    public void CompleteLevel()
    {
        var data = GetCurrentData();
        data.Level++;
        _yandexSaver.Save(data);
        _levelResultsPresenter.SetCleaned(_carpet.GetData());
        _levelResultsPresenter.PresentLevelResults();
    }

    public ProgressData GetCurrentData()
    {
        var data = new ProgressData();
        data.Level = _level;
        data.OpenedInstruments = _instrumentsProgressInitializer.GetData();
        return data;
    }
}
