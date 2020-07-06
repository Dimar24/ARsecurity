using UI.View.Answer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    [RequireComponent(typeof(Button))]
    public class AnswerButtonView : MonoBehaviour
    {
        [SerializeField] private AnswerView _answerView;
    
        private Button _button;

        public Button Button => _button;
        public AnswerView View => _answerView;
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
    }
}
