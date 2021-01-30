using System;
using Code.CharacterConfiguration;
using UnityEngine;

namespace Code.Folders
{
    public class MainFolder : MonoBehaviour
    {
        [SerializeField] private Character[] _characters;

        public Character SelectedCharacter => _characters[0];

        public static MainFolder Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}