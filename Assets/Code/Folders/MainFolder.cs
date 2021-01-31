using System;
using Code.CharacterConfiguration;
using Code.Files;
using UnityEngine;

namespace Code.Folders
{
    public class MainFolder : MonoBehaviour
    {
        [SerializeField] private Character[] _characters;
        [SerializeField] private Folder[] _folders;

        public Character SelectedCharacter
        {
            get => _characters[_selectedChar];
            set
            {
                int newIndex = Array.IndexOf(_characters, value);

                if (newIndex != _selectedChar)
                {
                    _folders[_selectedChar].Deselected();
                    _selectedChar = newIndex;
                }
            }
        }

        public Character[] Characters => _characters;

        public static MainFolder Instance;

        private int _selectedChar;
        private void Awake()
        {
            Instance = this;

            for (int i = 0; i < _folders.Length; i++)
            {
                _folders[i].Configure(_characters[i]);
            }

            _folders[0].Selected();
        }
    }
}