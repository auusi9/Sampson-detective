using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Map
{
    public class PinCreator : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject _pinPrefab;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Map _map;

        private PanelElement _lastPinCreated;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                return;
            }
            
            Vector3 position = _canvas.worldCamera.ScreenToWorldPoint(eventData.position);
            position.z = _canvas.transform.position.z;
            GameObject newPin = Instantiate(_pinPrefab, position, _pinPrefab.transform.rotation, transform);
            Pin pinCreated = newPin.GetComponent<Pin>();
            _lastPinCreated = pinCreated.PanelElement;
            _lastPinCreated.Configure(_canvas, GetComponent<RectTransform>());
            _map.AddPin(pinCreated);
            _lastPinCreated.OnPointerDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                return;
            }
            
            _lastPinCreated?.OnDrag(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                return;
            }
            
            _lastPinCreated?.OnBeginDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                return;
            }
            
            _lastPinCreated?.OnEndDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                return;
            }
            
            _lastPinCreated?.OnPointerUp(eventData);
            _lastPinCreated = null;
        }
    }
}