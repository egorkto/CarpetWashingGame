using System;
using System.Collections;
using UnityEngine;

public class Vacuum : Instrument
{
    public event Action Clean;

    [SerializeField] private float _cleanDelay;

    private Trash _target = null;
    private bool _canClean = true;

    protected override void Working(RaycastHit hit)
    {
        MoveTo(hit.point);
        if (_target != null && _canClean)
            StartCoroutine(TryClean(_target));
    }

    protected override void StopWorking()
    {
        _target = null;
    }

    protected override void Disable()
    {
        _canClean = true;
        StopAllCoroutines();
    }

    private void MoveTo(Vector3 point)
    {
        transform.position = new Vector3(point.x, transform.position.y, point.z);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (_target == null && _canClean && collider.TryGetComponent(out Trash trash))
            _target = trash;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_target != null && _target.gameObject == collider.gameObject)
            _target = null;
    }

    private IEnumerator TryClean(Trash trash)
    {
        _canClean = false;
        Debug.Log("Start");
        yield return new WaitForSeconds(_cleanDelay);
        if (_target != null && _target.gameObject == trash.gameObject)
        {
            Debug.Log(_target);
            trash.Delete();
            Clean?.Invoke();
        }
        Debug.Log("Stop");
        _canClean = true;
    }
}
