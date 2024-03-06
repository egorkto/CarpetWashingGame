using System;
using System.Collections;
using UnityEngine;

public class RepairTool : MonoBehaviour
{
    public event Action FinishRepair;

    [SerializeField] private Transform _needle;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _bordersOffset;

    private IRepairable _target;
    private Vector3[] _points;
    private int _nextPointIndex = 0;
    private bool _canRepair = true;

    public void Initialize(IRepairable target, Bounds bounds)
    {
        _target = target;
        bounds.size -= _bordersOffset;
        _points = new Vector3[] 
        {   new Vector3(bounds.max.x, _needle.position.y, bounds.min.z),
            new Vector3(bounds.max.x, _needle.position.y, bounds.max.z), 
            new Vector3(bounds.min.x, _needle.position.y, bounds.max.z), 
            new Vector3(bounds.min.x, _needle.position.y, bounds.min.z) };
        var startPoint = new Vector3(_points[_points.Length - 1].x, _needle.position.y, _points[_points.Length - 1].z);
        _needle.position = startPoint;
        _model.SetActive(true);
    }

    private void OnEnable()
    {
        if(_target != null && _target.Whole == false)
            StartCoroutine(Repair());
        else
            _model.SetActive(false);
    }

    private IEnumerator Repair()
    {
        while(_canRepair)
        {
            var point = new Vector3(_points[_nextPointIndex].x, _needle.position.y, _points[_nextPointIndex].z);
            _needle.transform.LookAt(point);
            _needle.transform.position = Vector3.MoveTowards(_needle.transform.position, point, _speed * Time.deltaTime);
            if(_needle.position == point)
            {
                if (_nextPointIndex == _points.Length - 1)
                {
                    _target.Repair();
                    FinishRepair?.Invoke();
                    _canRepair = false;
                    _model.SetActive(false);
                }
                else
                    _nextPointIndex++;
            }
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
