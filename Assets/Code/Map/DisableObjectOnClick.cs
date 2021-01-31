using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Map
{
    public class DisableObjectOnClick : MonoBehaviour
    {
        [SerializeField] private GameObject _targetGameObject;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(ToggleGameobject);
        }

        private void ToggleGameobject()
        {
            _targetGameObject.gameObject.SetActive(!_targetGameObject.activeSelf);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ToggleGameobject);
        }
    }
}