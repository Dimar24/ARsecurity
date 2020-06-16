using System.Collections;
using Subsystem.Question;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Text _questionText;
    [SerializeField] private AnswerButtonView[] _buttons;

    private void Awake()
    {
        StartCoroutine(QuestionRoutine());
    }

    private IEnumerator QuestionRoutine()
    {
        while (true)
        {
            var questionData = QuestionManager.GetQuestion(Random.Range(0, 3)).Value;
            
            _questionText.text = questionData.QuestionText;
            for (var i = 0; i < _buttons.Length; ++i)
            {
                _buttons[i].NumberText = i.ToString();
                _buttons[i].AnswerText = questionData.Answers[i].Text;
            }
            
            yield return new WaitForSeconds(3f);
        }
    }
}
