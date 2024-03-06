using System;
using System.Collections;
using UnityEngine;

public class WaterSprinkler : Instrument
{
    public event Action<float> Clean;

    [SerializeField] private float _changeStateDelay;
    [SerializeField] private float[] _alfaComponents;

    private Material _dirtyMaterial;
    private int _currentColorIndex = 0;
    private bool _cleaning;

    public void Initialize(Material material)
    {
        _dirtyMaterial = material;
    }

    protected override void Working(RaycastHit hit)
    {
        if (!_cleaning && _currentColorIndex < _alfaComponents.Length)
            StartCoroutine(Cleaning());
    }

    protected override void StopWorking()
    {
        StopAllCoroutines();
        _cleaning = false;
    }

    private IEnumerator Cleaning()
    {
        _cleaning = true;
        yield return new WaitForSeconds(_changeStateDelay);
        ChangeAlfa();
        _currentColorIndex++;
        _cleaning = false;
    }

    private void ChangeAlfa()
    {
        _dirtyMaterial.color = new Color(0, 0, 0, _alfaComponents[_currentColorIndex]);
        Clean?.Invoke(_dirtyMaterial.color.a);
    }
}
