using System;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public event Action StartMotion;
    public event Action<Vector3> Motion;
    public event Action EndMotion;

    [SerializeField] private CarpetInterectionHandler _handler;

    protected virtual void Enable() { }
    protected virtual void Disable() { }
    protected virtual void StartWorking() { }
    protected virtual void Working(RaycastHit hit) { }
    protected virtual void StopWorking() { }

    private void OnEnable()
    {
        Enable();
        _handler.StartInteract += OnStartInteract;
        _handler.Hold += OnHold;
        _handler.EndInteract += OnEndInteract;
    }

    private void OnDisable()
    {
        Disable();
        _handler.StartInteract -= OnStartInteract;
        _handler.Hold -= OnHold;
        _handler.EndInteract -= OnEndInteract;
    }

    private void OnStartInteract()
    {
        StartMotion?.Invoke();
        StartWorking();
    }

    private void OnHold(RaycastHit hit)
    {
        Motion?.Invoke(hit.point);
        Working(hit);
    }

    private void OnEndInteract()
    {
        EndMotion?.Invoke();
        StopWorking();
    }
}