using System.Collections;
using UnityEngine;

public class FoamSprinkler : Instrument
{
    [SerializeField] private TextureDrawer _drawer;
    [SerializeField] private int _sprinkRadius;
    [SerializeField] private int _foamRadius;
    [SerializeField] private float _foamSpawnDelay;
    [SerializeField] private int _pointsCount;
    [SerializeField] private Color _color;

    private Texture2D _foamTexture;
    private bool _canSpawn = true;

    public void Initialize(Texture2D foamTexture)
    {
        _foamTexture = foamTexture;
    }

    protected override void Working(RaycastHit hit)
    {
        TrySprink(hit);
    }

    protected override void Disable()
    {
        StopAllCoroutines();
        _canSpawn = true;
    }

    private void TrySprink(RaycastHit hit)
    {
        if(_canSpawn && _foamTexture != null)
        {
            Vector2 pixelUV = new Vector2(hit.textureCoord.x * _foamTexture.width, hit.textureCoord.y * _foamTexture.height);
            DrawFoam(pixelUV);
            StartCoroutine(WaitSpawnDelay());
        }
    }

    private void DrawFoam(Vector2 pixelUV)
    {
        var points = new Vector2[_pointsCount];
        for (var i = 0; i < points.Length; i++)
            points[i] = Random.insideUnitCircle * _sprinkRadius + pixelUV;
        foreach(var point in points)
            _drawer.DrawCircle(_foamTexture, _foamRadius, point, _color);
    }

    private IEnumerator WaitSpawnDelay()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(_foamSpawnDelay);
        _canSpawn = true;
    }
}
