using System;
using System.Collections;
using Subsystem.Question;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Text _questionText;
    [SerializeField] private AnswerButtonView[] _buttons;

    [SerializeField] private Color _defaultNumberColor;
    [SerializeField] private Color _defaultAnswerColor;
    [SerializeField] private Color _trueNumberColor;
    [SerializeField] private Color _trueAnswerColor;
    [SerializeField] private Color _falseNumberColor;
    [SerializeField] private Color _falseAnswerColor;

    [SerializeField] private Button _backButton;
    [SerializeField] private int _number = 10;
    [SerializeField] private float _durationReset = 1.0f;

    private int _count;
    private float _lastClicked;

    private QuestionData _questionData;
    private bool isClicked;

    private void Awake()
    {
        StartCoroutine(QuestionRoutine());
        for (var i = 0; i < _buttons.Length; ++i)
        {
            var temp = i;
            _buttons[i].Clicked += () => OnButtonClicked(temp);
        }
    }

    private IEnumerator QuestionRoutine()
    {
        while (true)
        {
            _questionData = QuestionManager.GetQuestion(Random.Range(0, QuestionManager.Count)).Value;

            _questionText.text = _questionData.QuestionText;
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i];
                button.NumberText = (i + 1).ToString();
                button.AnswerText = _questionData.Answers[i].Text;
                button.SetColor(_defaultNumberColor, _defaultAnswerColor);
                button.SetFrame(false);
            }

            isClicked = false;

            yield return new WaitForSeconds(3f);
        }
    }

    private void OnButtonClicked(int id)
    {
        if (!isClicked)
        {
            isClicked = true;
            _buttons[id].SetFrame(true);
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i];
                if (_questionData.Answers[i].IsCorrect)
                    button.SetColor(_trueNumberColor, _trueAnswerColor);
                else
                    button.SetColor(_falseNumberColor, _falseAnswerColor);
            }
        }
    }
}