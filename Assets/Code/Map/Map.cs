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
        Dictionary<Character, List<Pin>> _pins = new Dictionary<Character, List<Pin>>();

        private ComponentPool<PinLine> _linePools;

        private void Start()
        {
            _linePools = new ComponentPool<PinLine>(20, _lineRenderer.gameObject);
        }

        private void Update()
        {
            foreach (KeyValuePair<Character, List<Pin>> pinList in _pins)
            {
                if (pinList.Value.Count > 1)
                {
                    for (int i = 1; i < pinList.Value.Count; i++)
                    {
                        SetLineRendererPositions(pinList, i);
                    }
                }
            }
        }

        public void AddPin(Pin pin)
        {
            if (!_pins.ContainsKey(MainFolder.Instance.SelectedCharacter))
            {
                _pins.Add(MainFolder.Instance.SelectedCharacter, new List<Pin>());
            }


            _pins[MainFolder.Instance.SelectedCharacter].Add(pin);
            pin.RightClickEvent += DestroyPin;
        }

        private void SetLineRendererPositions(KeyValuePair<Character, List<Pin>> pinList, int i)
        {
            PinLine pinLine = GetPinLine(pinList.Value[i]);
            pinLine.gameObject.SetActive(true);

            pinLine.Configure(pinList.Key.Color, new[]
            {
                pinList.Value[i - 1].transform.position,
                pinList.Value[i].transform.position
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
            }
        }
    }
}