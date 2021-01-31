using UnityEngine;

namespace Code.Map.PointsOfInterest
{
    public class POI : MonoBehaviour
    {
        [SerializeField] private string _id;
        public string ID => _id;
    }
}