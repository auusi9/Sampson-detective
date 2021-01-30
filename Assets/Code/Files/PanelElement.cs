using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Shadow _shadow;

    public event Action RightClickEvent;
    
    private Canvas _canvas;
    private RectTransform _parent;

    public void OnInstantiate(Canvas canvas, RectTransform parent)
    {
        _canvas = canvas;
        _parent = parent;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        
        _shadow.enabled = true;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        
        Vector3[] fourCornersArray = new Vector3[4];
        _parent.GetWorldCorners(fourCornersArray);

        Vector3 pos = _rectTransform.position;

        pos.x = Mathf.Clamp(pos.x, fourCornersArray[0].x, fourCornersArray[2].x);
        pos.y = Mathf.Clamp(pos.y, fourCornersArray[0].y, fourCornersArray[2].y);

        _rectTransform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        
        _shadow.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClickEvent?.Invoke();
        }
    }
}
