using System;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour, IRepairable
{
    public bool Whole => _meshFilter.sharedMesh == _wholeMesh;
    public Trash[] Trash => _trash;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshCollider _meshCollider;

    private Mesh _wholeMesh;
    private Trash[] _trash = new Trash[0];

    public void Initialize(CarpetData data, Mesh wholeMesh)
    {
        SetData(data);
        _wholeMesh = wholeMesh;
        _meshCollider.sharedMesh = wholeMesh;
    }

    public void SetData(CarpetData data)
    {
        _meshRenderer.SetMaterials(new List<Material> { data.Drawing, data.DirtyAlfa, data.Dirty2, data.Dirty1, data.Foam1, data.Foam2 });
        _meshFilter.sharedMesh = data.Mesh;
    }

    public CarpetData GetData()
    {
        var data = new CarpetData();
        data.Mesh = _meshFilter.sharedMesh;
        data.Drawing = _meshRenderer.materials[0];
        data.DirtyAlfa = _meshRenderer.materials[1];
        data.Dirty2 = _meshRenderer.materials[2];
        data.Dirty1 = _meshRenderer.materials[3];
        data.Foam1 = _meshRenderer.materials[4];
        data.Foam2 = _meshRenderer.materials[5];
        return data;
    }

    public void Repair()
    {
        _meshFilter.sharedMesh = _wholeMesh;
    }

    public void SetTrash(Trash[] trash)
    {
        _trash = trash;
    }

    public Bounds GetBounds()
    {
        var bounds = _wholeMesh.bounds;
        bounds.size = new Vector3(bounds.size.x * transform.localScale.x, bounds.size.y, bounds.size.z * transform.localScale.z);
        bounds.center += transform.position;
        return bounds;
    }
}
