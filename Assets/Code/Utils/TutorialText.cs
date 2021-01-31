using System;
using TMPro;
using UnityEngine;

namespace Code.Utils
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private float _timeBetweenLetters = 0.2f;

        private float _time;
        private int _counter = 0;
        private int _totalVisibleCharacters = 0;

        private void Start()
        {
            _textMeshPro.ForceMeshUpdate();
            _totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
            _textMeshPro.maxVisibleCharacters = 0;
        }

        private void Update()
        {
            if (_counter >= _totalVisibleCharacters)
            {
                return;
            }
            
            if (_timeBetweenLetters < _time)
            {
                _textMeshPro.maxVisibleCharacters = _counter;
                _counter += 1;
                _time = 0f;
            }
                
            _time += Time.deltaTime;
        }
    }
}