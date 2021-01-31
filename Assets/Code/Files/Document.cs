using UnityEngine;

namespace Code.Files
{
    public class Document : MonoBehaviour
    {
        [SerializeField] private PanelElement _panelElement;
        
        public void Configure(Canvas canvas, RectTransform parent)
        {
            _panelElement.Configure(canvas, parent);
        }
    }
}