using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Utils
{
    public class OpenSceneButton : MonoBehaviour
    {
        [SerializeField] private int _scene;
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(OpenScene);

            if (PlayerPrefs.GetInt("LevelMax", 1) < _scene)
            {
                _button.interactable = false;
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OpenScene);
        }

        public void OpenScene()
        {
            SceneManager.LoadScene(_scene);
        }
    }
}