using Code.Map;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.FinalMenu
{
    public class FinalResultsMenu : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _lostImage;
        [SerializeField] private Image _foundImage;
        [SerializeField] private TextMeshProUGUI _correctText;
        [SerializeField] private TextMeshProUGUI _incorrectText;
        [SerializeField] private TextMeshProUGUI _total;

        public void Configure(Score score)
        {
            float perc = (float)score.Value / score.MaxValue;

            _fillImage.fillAmount = perc;

            _correctText.text = score.CorrectOnes.ToString();
            _incorrectText.text = score.IncorrectOnes.ToString();
            _total.text = (score.IncorrectOnes + score.CorrectOnes).ToString();
            
            if (perc > 0.8f)
            {
                PlayerPrefs.SetInt("LevelMax", 2);
                _lostImage.gameObject.SetActive(false);
                _foundImage.gameObject.SetActive(true);
            }
            else
            {
                _lostImage.gameObject.SetActive(true);
                _foundImage.gameObject.SetActive(false);
            }
        }

        public void Menu()
        {
            SceneManager.LoadScene(0);
        }
        
    }
}