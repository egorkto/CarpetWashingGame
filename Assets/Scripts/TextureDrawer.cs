using UnityEngine;

public class TextureDrawer : MonoBehaviour
{
    public void DrawRect(Texture2D texture, Vector2 size, Vector2 origin, Color color)
    {
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                var point = origin + new Vector2(x, y);
                if (point.x >= 0 && point.x < texture.width && point.y >= 0 && point.y < texture.height)
                    texture.SetPixel((int)point.x, (int)point.y, color);
            }
        }
        texture.Apply();
    }

    public void DrawCircle(Texture2D texture, int radius, Vector2 origin, Color color)
    {
        for (int y = 0; y < radius; y++)
        {
            for (int x = 0; x < radius; x++)
            {
                float x2 = Mathf.Pow(x - radius / 2, 2);
                float y2 = Mathf.Pow(y - radius / 2, 2);
                float r2 = Mathf.Pow(radius / 2 - 0.5f, 2);

                if (x2 + y2 < r2)
                {
                    var point = origin + new Vector2(x, y);
                    if (point.x >= 0 && point.x < texture.width && point.y >= 0 && point.y < texture.height)
                    {
                        texture.SetPixel((int)point.x, (int)point.y, color);
                    }
                }
            }
        }
        texture.Apply();
    }

    public int DrawIntersection(Texture2D first, Texture2D second, Vector2 origin, Vector2 area, Color color)
    {
        var intersection = 0;
        for (int y = 0; y < area.y; y++)
        {
            for (int x = 0; x < area.x; x++)
            {
                int pixelX = (int)(origin.x + x - area.x / 2);
                int pixelY = (int)(origin.y + y - area.y / 2);
                if (pixelX >= 0 && pixelX < first.width && pixelY >= 0 && pixelY < first.height && first.GetPixel(pixelX, pixelY) != Color.clear)
                {
                    if(second.GetPixel(pixelX, pixelY) != Color.clear)
                        intersection++;
                    first.SetPixel(pixelX, pixelY, color);
                    second.SetPixel(pixelX, pixelY, color);
                }
            }
        }
        first.Apply();
        second.Apply();
        return intersection;
    }
}
