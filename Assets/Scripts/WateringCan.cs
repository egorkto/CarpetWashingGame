using System.Collections;
using UnityEngine;

public class WateringCan : Instrument
{
    [SerializeField] private TextureDrawer _drawer;
    [SerializeField] private int _fixedSize;
    [SerializeField] private int _radius;
    [SerializeField] private int _randomPointsSize;
    [SerializeField] private int _randomPointsSpawnRadius;
    [SerializeField] private int _randomPointsCount;
    [SerializeField] private float _spawnRandomPointsDelay;
    [SerializeField] private Color _color;

    private Texture2D _foamTexture;
    private bool _canSpawnRandomPoints = true;
    private Vector2[] _fixedOffsets;

    public void Initialize(Texture2D foamTexture)
    {
        _foamTexture = foamTexture;
    }

    private void Start()
    {
        _fixedOffsets = new Vector2[]
        {
            Vector2.zero,
            new Vector2(-_radius, 0),
            new Vector2(_radius, 0),
            new Vector2(0, -_radius),
            new Vector2(0, _radius),
            new Vector2((_radius * Mathf.Cos(45)), (_radius * Mathf.Sin(45))),
            new Vector2(-(_radius * Mathf.Cos(45)), -(_radius * Mathf.Sin(45))),
            new Vector2(-(_radius * Mathf.Cos(45)), (_radius * Mathf.Sin(45))),
            new Vector2((_radius * Mathf.Cos(45)), -(_radius * Mathf.Sin(45))),
        };
    }

    protected override void Working(RaycastHit hit)
    {
        Spawn(hit);
    }

    private void Spawn(RaycastHit hit)
    {
        if(_foamTexture != null)
        {
            Vector2 pixelUV = new Vector2(hit.textureCoord.x * _foamTexture.width, hit.textureCoord.y * _foamTexture.height);
            DrawFixed(pixelUV);
            if (_canSpawnRandomPoints)
            {
                DrawRandoms(pixelUV);
                StartCoroutine(WaitRandomSpawnDelay());
            }
        }
    }

    private void DrawFixed(Vector2 origin)
    {
        foreach(var offset in _fixedOffsets)
        {
            var point = origin + offset;
            _drawer.DrawCircle(_foamTexture, _fixedSize, point, _color);
        }
    }

    private void DrawRandoms(Vector2 origin)
    {
        for (int i = 0; i < _randomPointsCount; i++)
        {
            var random = Random.insideUnitCircle * _randomPointsSpawnRadius;
            var point = origin + random;
            _drawer.DrawCircle(_foamTexture, _randomPointsSize, point, _color);
        }
    }

    private IEnumerator WaitRandomSpawnDelay()
    {
        _canSpawnRandomPoints = false;
        yield return new WaitForSeconds(_spawnRandomPointsDelay);
        _canSpawnRandomPoints = true;
    }
}
