using Code.Map;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.FinalMenu
{
    public class FinalResultsMenu : MonoBehaviour
    {
        [SerializeField] private Image[] _stars;
        [SerializeField] private Color _colorWon;
        [SerializeField] private Color _colorLost;
        [SerializeField] private TextMeshProUGUI _correctText;
        [SerializeField] private TextMeshProUGUI _incorrectText;

        public void Configure(Score score)
        {
            float perc = (float)score.Value / score.MaxValue;

            for (var i = 0; i < _stars.Length; i++)
            {
                if (i < perc * _stars.Length)
                {
                    _stars[i].color = _colorWon;
                }
                else
                {
                    _stars[i].color = _colorLost;
                }
            }

            _correctText.text = score.CorrectOnes.ToString();
            _incorrectText.text = score.IncorrectOnes.ToString();

            if (perc > 0.6f)
            {
                PlayerPrefs.SetInt("LevelMax", 2);
            }
        }

        public void Menu()
        {
            SceneManager.LoadScene(0);
        }
        
    }
}