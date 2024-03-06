using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarpetCreater : MonoBehaviour
{
    [SerializeField] private Carpet _carpet;
    [SerializeField] private Transform _carpetPoint;
    [SerializeField] private Material _drawingMaterial;
    [SerializeField] private Material _pixelDirtyMaterial;
    [SerializeField] private Material _alfaDirtyMaterial;
    [SerializeField] private Material _foamMaterial;
    [SerializeField] private CarpetSetup _1by1Setup;
    [SerializeField] private CarpetSetup _1by15Setup;
    [SerializeField] private CarpetSetup _1by2Setup;
    [SerializeField] private Trash[] _trash;
    [SerializeField] private int _trashCount;
    [SerializeField] private Vector2 _trashSpawnOffset;
    [SerializeField] [Range(0, 100)] private int _1by1Chance;
    [SerializeField] [Range(0, 100)] private int _1by15Chance;
    [SerializeField] [Range(0, 100)] private int _1by2Chance;
    [SerializeField] [Range(0, 100)] private int _dirty1Chance;
    [SerializeField] [Range(0, 100)] private int _dirty2Chance;
    [SerializeField] [Range(0, 100)] private int _alfaDirtyChance;
    [SerializeField] [Range(0, 100)] private int _trashChance;
    [SerializeField] [Range(0, 100)] private int _tornChance;

    private float _spawnYOffset = 0.5f;

    public Carpet CreateCarpet(List<InstrumentType> types)
    {
        var carpet = Instantiate(_carpet, _carpetPoint.position, Quaternion.identity);
        var setup = GetRandomSetup();
        var data = GetNewData();
        foreach (var type in types)
            SetChance(type);
        data.Drawing.mainTexture = setup.CarpetTextures[Random.Range(0, setup.CarpetTextures.Length)];
        if (Random.Range(0, 100) < _dirty1Chance)
            data.Dirty1.mainTexture = CopyTexture(setup.DirtyTextures1[Random.Range(0, setup.DirtyTextures1.Length)]);
        if (Random.Range(0, 100) < _dirty2Chance)
            data.Dirty2.mainTexture = CopyTexture(setup.DirtyTextures2[Random.Range(0, setup.DirtyTextures2.Length)]);
        if (Random.Range(0, 100) < _alfaDirtyChance)
            data.DirtyAlfa.color = new Color(0, 0, 0, 0.9f);
        data.Foam1.mainTexture = GetClear(data.Dirty1.mainTexture.width, data.Dirty1.mainTexture.height);
        data.Foam2.mainTexture = GetClear(data.Dirty2.mainTexture.width, data.Dirty2.mainTexture.height);
        if (Random.Range(0, 100) < _tornChance)
            data.Mesh = setup.TornMeshs[Random.Range(0, setup.TornMeshs.Length)];
        else
            data.Mesh = setup.WholeMesh;
        carpet.Initialize(data, setup.WholeMesh);
        if (Random.Range(0, 100) < _trashChance)
            carpet.SetTrash(CreateTrash(carpet.GetBounds()));
        return carpet;
    }

    private CarpetSetup GetRandomSetup()
    {
        var random = Random.Range(0, 100);
        if (random <= _1by1Chance)
            return _1by1Setup;
        else if (random - _1by15Chance <= _1by15Chance)
            return _1by15Setup;
        else
            return _1by2Setup;
    }

    private CarpetData GetNewData()
    {
        var data = new CarpetData();
        data.Drawing = new Material(_drawingMaterial);
        data.Dirty1 = new Material(_pixelDirtyMaterial);
        data.Dirty2 = new Material(_pixelDirtyMaterial);
        data.DirtyAlfa = new Material(_alfaDirtyMaterial);
        data.Foam1 = new Material(_foamMaterial);
        data.Foam2 = new Material(_foamMaterial);
        return data;
    }

    private void SetChance(InstrumentType type)
    {
        switch (type)
        {
            case InstrumentType.WaterSprinkler:
                _alfaDirtyChance = 100;
                break;
            case InstrumentType.FoamCleaner:
                _dirty1Chance = 100;
                break;
            case InstrumentType.ChemicalCleaner:
                _dirty2Chance = 100;
                break;
            case InstrumentType.Vacuum:
                _trashChance = 100;
                break;
            case InstrumentType.RepairTool:
                _tornChance = 100;
                break;
        }
    }

    private Trash[] CreateTrash(Bounds bounds)
    {
        var trash = new Trash[_trashCount];
        for (int i = 0; i < trash.Length; i++)
        {
            var x = Random.Range(bounds.min.x + _trashSpawnOffset.x, bounds.max.x - _trashSpawnOffset.x);
            var y = bounds.center.y + _spawnYOffset;
            var z = Random.Range(bounds.min.z + _trashSpawnOffset.y, bounds.max.z - _trashSpawnOffset.y);
            var randomTrash = _trash[Random.Range(0, _trash.Length)];
            trash[i] = Instantiate(randomTrash, new Vector3(x, y, z), Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
        return trash;
    }

    private Texture2D CopyTexture(Texture2D original)
    {
        var texture = new Texture2D(original.width, original.height);
        texture.SetPixels(original.GetPixels());
        texture.Apply();
        return texture;
    }

    private Texture2D GetClear(int width, int height)
    {
        var texture = new Texture2D(width, height);
        for (var x = 0; x < texture.width; x++)
            for (var y = 0; y < texture.height; y++)
                texture.SetPixel(x, y, Color.clear);
        texture.Apply();
        return texture;
    }
}
