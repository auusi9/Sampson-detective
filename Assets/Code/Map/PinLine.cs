using UnityEngine;

namespace Code.Map
{
    public class PinLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        public void Configure(Color color, Vector3[] positions)
        {
            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
        }
    }
}