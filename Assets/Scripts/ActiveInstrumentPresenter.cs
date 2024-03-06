using System;
using UnityEngine;

public class ActiveInstrumentPresenter : InstrumentPresenter
{
    public event Action<ActiveInstrumentPresenter> Click;

    [SerializeField] private Outline _outline;
    [SerializeField] [Range(0, 10)] private float _activeOutlineWidth;

    private bool _active;

    public void Activate()
    {
        _active = true;
        _outline.OutlineWidth = _activeOutlineWidth;
    }

    public void Disactivate()
    {
        _active = false;
        _outline.OutlineWidth = 0;
    }

    protected override void Initialize()
    {
        _outline.enabled = true;
        _outline.OutlineWidth = 0;
    }

    protected override void ClickReact()
    {
        Click?.Invoke(this);
    }

    protected override void EnterPointer()
    {
        _outline.OutlineWidth = _activeOutlineWidth;
    }

    protected override void ExitPointer()
    {
        if (!_active)
            _outline.OutlineWidth = 0;
    }
}
