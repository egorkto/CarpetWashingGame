using System;
using UnityEngine;

public class CarpetInterectionHandler : MonoBehaviour
{
    public event Action StartInteract;
    public event Action<RaycastHit> Hold;
    public event Action EndInteract;

    [SerializeField] private Camera _camera;

    private bool _interacting;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.transform.parent != null && hit.collider.transform.parent.TryGetComponent(out Carpet carpet))
                {
                    if (_interacting)
                    {
                        Hold?.Invoke(hit);
                    }
                    else
                    {
                        StartInteract?.Invoke();
                        _interacting = true;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndInteract?.Invoke();
            _interacting = false;
        }
    }
}
