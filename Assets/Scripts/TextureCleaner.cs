using System;
using UnityEngine;

public class TextureCleaner : Instrument
{
    public event Action<int> Clean;

    [SerializeField] private TextureDrawer _drawer;
    [SerializeField] private Vector2 _clearRange;

    private Texture2D _foamTexture;
    private Texture2D _dirtyTexture;

    public void Initialize(Texture2D foamTexture, Texture2D dirtyTexture)
    {
        _foamTexture = foamTexture;
        _dirtyTexture = dirtyTexture;
    }

    protected override void Working(RaycastHit hit)
    {
        TryClear(hit);
    }

    private void TryClear(RaycastHit hit)
    {
        if(_foamTexture != null && _dirtyTexture != null)
        {
            Vector2 pixelUV = new Vector2(hit.textureCoord.x * _foamTexture.width, hit.textureCoord.y * _foamTexture.height);
            var cleaned = _drawer.DrawIntersection(_foamTexture, _dirtyTexture, pixelUV, _clearRange, Color.clear);
            Clean?.Invoke(cleaned);
        }
    }
}
