using UnityEngine;

namespace Code.CharacterConfiguration
{
    [CreateAssetMenu(menuName = "Create Character", fileName = "(NAME)Character", order = 0)]
    public class Character : ScriptableObject
    {
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _spriteShape;

        public Color Color => _color;
        public Sprite SpriteShape => _spriteShape;
    }
}