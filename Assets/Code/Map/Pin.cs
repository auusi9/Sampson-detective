using System;
using UnityEngine;

namespace Code.Map
{
    public class Pin : MonoBehaviour
    {
        [SerializeField] private PanelElement _panelElement;
        public event Action<Pin> RightClickEvent;

        public PanelElement PanelElement => _panelElement;
        public PinLine PinLine;

        private void Start()
        {
            _panelElement.RightClickEvent += RightButtonClicked;
        }

        private void OnDestroy()
        {
            _panelElement.RightClickEvent -= RightButtonClicked;
        }

        private void RightButtonClicked()
        {
            RightClickEvent?.Invoke(this);
        }
    }
}