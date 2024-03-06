using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slider : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectMask2D _mask;
    [SerializeField] private RectTransform _border;
    [SerializeField] private float _slideAreaSizeMultiplier;

    private Vector2 _xBorders;

    private void Start()
    {
        _xBorders = new Vector2(_mask.rectTransform.rect.width / 2 * (1 - _slideAreaSizeMultiplier), _mask.rectTransform.rect.width / 2 * ( 1 + _slideAreaSizeMultiplier));
        _mask.padding = new Vector4(0, 0, _mask.rectTransform.rect.size.x / 2, 0);
        _border.anchoredPosition = new Vector2(-_mask.padding.z, _border.anchoredPosition.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var positionX = Mathf.Clamp(_mask.padding.z - eventData.delta.x, _xBorders.x, _xBorders.y);
        _mask.padding = new Vector4(0, 0, positionX, 0);
        _border.anchoredPosition = new Vector2(-_mask.padding.z, _border.anchoredPosition.y);
    }
}
