using System;
using Code.CharacterConfiguration;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Map
{
    public class Pin : MonoBehaviour
    {
        [SerializeField] private PanelElement _panelElement;
        [SerializeField] private Image _background;
        [SerializeField] private Image _shape;
        [SerializeField] private Transform _lineOrigin;
        public event Action<Pin> RightClickEvent;

        public Transform LineOrigin => _lineOrigin;

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

        public void Configure(Character character)
        {
            //_background.color = character.Color;
            _shape.sprite = character.SpriteShape;
        }

        private void RightButtonClicked()
        {
            RightClickEvent?.Invoke(this);
        }
    }
}