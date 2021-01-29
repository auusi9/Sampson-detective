using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _parent;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Shadow _shadow;

    private Vector3 off;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _shadow.enabled = true;
        transform.SetAsLastSibling();
        Vector3 position = _canvas.worldCamera.ScreenToWorldPoint(eventData.position);
        position.z = _canvas.transform.position.z;
        off = position - transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        
        if(_rectTransform.anchoredPosition.x < _parent.rect.x)
        {
            _rectTransform.anchoredPosition = new Vector2(-_parent.rect.x, _rectTransform.anchoredPosition.y);
        }

        if (_rectTransform.anchoredPosition.y < _parent.rect.y)
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, -_parent.rect.y);
        }

        if (_rectTransform.anchoredPosition.x > _parent.rect.x + _parent.rect.width)
        {
            _rectTransform.anchoredPosition = new Vector2(_parent.rect.x, _rectTransform.anchoredPosition.y);
        }

        if (_rectTransform.anchoredPosition.y > _parent.rect.y + _parent.rect.height)
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _parent.rect.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _shadow.enabled = false;
    }
}
