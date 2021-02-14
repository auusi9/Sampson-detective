using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Code.Pointer
{
    public class PointerHandler : MonoBehaviour
    {
        [SerializeField] private Texture2D _cursorHovered;
        [SerializeField] private Texture2D _cursorClickedPanel;
        [SerializeField] private Texture2D _cursorClicked;
        
        private List<int> _panelsHovered = new List<int>();
        private List<int> _panelsClicked = new List<int>();

        private bool _isClicked;

        public static PointerHandler Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void PanelHovered(int panel)
        {
            _panelsHovered.Add(panel);

            SetPointerImage();
        }

        public void PanelStoppedHovering(int panel)
        {
            if(_panelsHovered.Contains(panel))
            {
                _panelsHovered.Remove(panel);
            }
            
            SetPointerImage();
        }

        public void PanelClicked(int panel)
        {
            _panelsClicked.Add(panel);
            
            SetPointerImage();
        }

        public void PanelReleased(int panel)
        {
            if(_panelsClicked.Contains(panel))
            {
                _panelsClicked.Remove(panel);
            }
            
            SetPointerImage();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (_isClicked == false)
                {
                    _isClicked = true;
                    SetPointerImage();
                }
            }
            else
            {
                _isClicked = false;
            }

            SetPointerImage();
        }

        private void SetPointerImage()
        {
            if (_panelsClicked.Count > 0)
            {
                Cursor.SetCursor(_cursorClickedPanel, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (_panelsHovered.Count > 0)
            {
                Cursor.SetCursor(_cursorHovered, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (_isClicked)
            {
                Cursor.SetCursor(_cursorClicked, Vector2.zero, CursorMode.Auto);
                return;
            }
            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}