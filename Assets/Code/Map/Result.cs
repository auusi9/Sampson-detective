using System.Collections.Generic;
using System.Linq;
using Code.CharacterConfiguration;
using Code.FinalMenu;
using Code.Folders;
using Code.Map.PointsOfInterest;
using UnityEngine;

namespace Code.Map
{
    public class Result : MonoBehaviour
    {
        [SerializeField] private Map _map;
        [SerializeField] private FinalResultsMenu _finalResultsMenu;
        [SerializeField] private POI[] _pois;
        [SerializeField] private float _distanceThreshold;
        [SerializeField] private int _goodScore;
        [SerializeField] private int _badScore;

        public void CalculateResult()
        {
            Character[] characters = MainFolder.Instance.Characters;

            Score score = new Score();
            foreach (var character in characters)
            {
                score += CalculateCharacterScore(character);
            }

            FinalResultsMenu menu = Instantiate(_finalResultsMenu, _finalResultsMenu.transform.position,
                _finalResultsMenu.transform.rotation);
            
            menu.Configure(score);
            
            Debug.Log("Score " + score.Value + " Correct ones: " + score.CorrectOnes + " Incorrect ones: " + score.IncorrectOnes);
        }

        private Score CalculateCharacterScore(Character character)
        {
            Score score = new Score();
            List<Pin> pins = _map.GetCharacterPins(character);
            
            for (int i = 0; i < pins.Count; i++)
            {
                if (i > character.PoiSolution.Length - 1)
                {
                    score.Value -= _badScore;
                    score.IncorrectOnes++;
                    continue;
                }
                
                POI poi = _pois.FirstOrDefault(x => character.PoiSolution[i] == x.ID);

                if (poi == null)
                {
                    Debug.LogError("POI: " + character.PoiSolution[i] + " not found");
                    continue;
                }

                if (Vector2.Distance(poi.transform.position, pins[i].transform.position) < _distanceThreshold)
                {
                    score.Value += _goodScore;
                    score.CorrectOnes++;
                }
                else
                {
                    score.Value -= _badScore;
                    score.IncorrectOnes++;
                }
            }

            if (pins.Count < character.PoiSolution.Length)
            {
                int diff = character.PoiSolution.Length - pins.Count;
                
                score.Value -= _badScore * diff;
            }

            score.MaxValue = _goodScore * character.PoiSolution.Length;
            return score;
        }
    }

    public struct Score
    {
        public int Value;
        public int MaxValue;
        public int CorrectOnes;
        public int IncorrectOnes;

        public static Score operator +(Score a, Score b)
        {
            a.Value += b.Value;
            a.MaxValue += b.MaxValue;
            a.CorrectOnes += b.CorrectOnes;
            a.IncorrectOnes += b.IncorrectOnes;

            return a;
        }
    }
}