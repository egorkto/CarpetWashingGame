using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class InstrumentPresenter : MonoBehaviour
{
    public static event Action<InstrumentType> Click;

    public GameObject Instrument => _instrument;
    public bool Opened => _opened;
    public InstrumentType Type => _type;

    [SerializeField] private GameObject _instrument;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Image _closeIcon;
    [SerializeField] private float _closeColorMultiplier;
    [SerializeField] private float _openColorMultiplier;
    [SerializeField] private InstrumentType _type;

    private bool _opened = true;

    protected abstract void Initialize();

    protected virtual void ClickReact() {}
    protected virtual void EnterPointer() {}
    protected virtual void ExitPointer() {}

    public void Open()
    {
        _opened = true;
        _closeIcon.gameObject.SetActive(false);
        Lighten();
        Initialize();
    }

    public void Close()
    {
        _opened = false;
        _closeIcon.gameObject.SetActive(true);
        Darken();
    }

    private void OnMouseDown()
    {
        if (_opened)
            ClickReact();
        else
            Click?.Invoke(_type);
    }

    private void OnMouseEnter()
    {
        if (_opened)
            EnterPointer();
    }

    private void OnMouseExit()
    {
        if (_opened)
            ExitPointer();
    }

    private void Darken()
    {
        foreach (var material in _renderer.materials)
            material.color *= _closeColorMultiplier;
    }

    private void Lighten()
    {
        foreach (var material in _renderer.materials)
            material.color *= _openColorMultiplier;
    }
}

public enum InstrumentType
{
    None,
    WaterSprinkler,
    FoamCleaner,
    ChemicalCleaner,
    Vacuum,
    RepairTool
}
