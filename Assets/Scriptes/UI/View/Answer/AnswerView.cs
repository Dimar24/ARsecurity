using UnityEngine;
using UnityEngine.UI;

namespace UI.View.Answer
{
    public class AnswerView : MonoBehaviour
    {
        [SerializeField] private Text _numberText;
        [SerializeField] private Text _answerText;
        [SerializeField] private GameObject _answerFrameImage;
        [SerializeField] private Image _numberImage;
        [SerializeField] private Image _answerImage;
    

        public string NumberText
        {
            get => _numberText.text;
            set => _numberText.text = value;
        }

        public string AnswerText
        {
            get => _answerText.text;
            set => _answerText.text = value;
        }

        public void SetFrame(bool isActive)
        {
            _answerFrameImage.SetActive(isActive);
        }

        public void SetColor(Color numberColor, Color answerColor)
        {
            _numberImage.color = numberColor;
            _answerImage.color = answerColor;
        }
    }
}