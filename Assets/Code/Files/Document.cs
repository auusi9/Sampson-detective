using UnityEngine;

namespace Code.Files
{
    public class Document : MonoBehaviour
    {
        [SerializeField] private PanelElement _panelElement;
        [SerializeField] private string _targetId;

        public string TargetId => _targetId;

        public void Configure(Canvas canvas, RectTransform parent)
        {
            _panelElement.Configure(canvas, parent);
        }
    }
}