using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Map
{
    public class DisableObjectOnClick : MonoBehaviour
    {
        [SerializeField] private GameObject[] _targetGameObject;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(ToggleGameobject);
        }

        private void ToggleGameobject()
        {
            foreach (var go in _targetGameObject)
            {
                go.SetActive(!go.activeSelf);
            }
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ToggleGameobject);
        }
    }
}