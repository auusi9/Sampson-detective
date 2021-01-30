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
                _documents[i] = Instantiate(_character.Documents[i], transform.parent.position, _character.Documents[i].transform.rotation, _documentsParent);
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
            
            foreach (var document in _documents)
            {
                document.gameObject.SetActive(true);
            }
        }

        public void Deselected()
        {
            foreach (var document in _documents)
            {
                document.gameObject.SetActive(false);
            }
        }
    }
}