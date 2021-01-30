using Code.Files;
using UnityEngine;

namespace Code.CharacterConfiguration
{
    [CreateAssetMenu(menuName = "Create Character", fileName = "(NAME)Character", order = 0)]
    public class Character : ScriptableObject
    {
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _spriteShape;
        [SerializeField] private Document[] _documents;

        public Color Color => _color;
        public Sprite SpriteShape => _spriteShape;

        public Document[] Documents => _documents;
    }
}