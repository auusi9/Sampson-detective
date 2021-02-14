using Code.CharacterConfiguration;
using Code.Folders;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Files
{
    public class Folder : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _documentsParent;
        [SerializeField] private Canvas _canvas;
        private Document[] _documents;
        private Character _character;
        
        public void Configure(Character character)
        {
            _character = character;
            _documents = new Document[_character.Documents.Length];
            for (var i = 0; i < _character.Documents.Length; i++)
            {
                _documents[i] = Instantiate(_character.Documents[i], _documentsParent.position + Vector3.one * Random.Range(-5f, 5f), Quaternion.Euler(0,0,Random.Range(-30, 30)), _documentsParent);
                _documents[i].Configure(_canvas, _documentsParent);
                Deselected();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            MainFolder.Instance.SelectedCharacter = _character;
            Selected();
        }

        public void Selected()
        {
            transform.parent.SetAsLastSibling();
        }

        public void Deselected()
        {
        }
    }
}