using System;
using System.Collections.Generic;
using System.Linq;
using Code.CharacterConfiguration;
using Code.Folders;
using Code.Utils;
using UnityEngine;

namespace Code.Map
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _linePoolParent;
        Dictionary<Character, List<Pin>> _pins = new Dictionary<Character, List<Pin>>();

        private ComponentPool<PinLine> _linePools;

        private void Start()
        {
            _linePools = new ComponentPool<PinLine>(3, _lineRenderer.gameObject, _linePoolParent);
        }

        private void Update()
        {
            List<Pin> allPins = new List<Pin>();
            foreach (KeyValuePair<Character, List<Pin>> pinList in _pins)
            {
                allPins.AddRange(pinList.Value);
                if (pinList.Value.Count > 1)
                {
                    for (int i = 1; i < pinList.Value.Count; i++)
                    {
                        SetLineRendererPositions(pinList, i);
                    }
                }
            }
            
            allPins.Sort((x, y) => y.transform.position.y.CompareTo(x.transform.position.y));

            for (var i = 0; i < allPins.Count; i++)
            {
                var pin = allPins[i];
                pin.transform.SetSiblingIndex(i);
            }
        }

        public void AddPin(Pin pin)
        {
            if (!_pins.ContainsKey(MainFolder.Instance.SelectedCharacter))
            {
                _pins.Add(MainFolder.Instance.SelectedCharacter, new List<Pin>());
            }

            _pins[MainFolder.Instance.SelectedCharacter].Add(pin);
            pin.Configure(MainFolder.Instance.SelectedCharacter);
            pin.RightClickEvent += DestroyPin;
        }

        private void SetLineRendererPositions(KeyValuePair<Character, List<Pin>> pinList, int i)
        {
            PinLine pinLine = GetPinLine(pinList.Value[i]);
            pinLine.gameObject.SetActive(true);

            pinLine.Configure(pinList.Key.Color, new[]
            {
                pinList.Value[i - 1].LineOrigin.position,
                pinList.Value[i].LineOrigin.position
            });
        }

        private PinLine GetPinLine(Pin pin)
        {
            if (!pin.PinLine)
            {
                pin.PinLine = _linePools.GetComponent();
                pin.PinLine.gameObject.SetActive(true);
            }
            
            return pin.PinLine;
        }

        private void DestroyPin(Pin pin)
        {
            pin.RightClickEvent -= DestroyPin;
            foreach (KeyValuePair<Character, List<Pin>> pinList in _pins)
            {
                if (pinList.Value.Contains(pin))
                {
                    int index = pinList.Value.IndexOf(pin);
                    if (index < pinList.Value.Count - 1)
                    {
                        ReturnPinLine(pinList.Value[index + 1]);
                    }
                    pinList.Value.Remove(pin);
                }
            }

            ReturnPinLine(pin);
            
            Destroy(pin.gameObject);
        }

        private void ReturnPinLine(Pin pin)
        {
            if (pin.PinLine != null)
            {
                _linePools.ReturnMono(pin.PinLine);
                pin.PinLine = null;
            }
        }
    }
}