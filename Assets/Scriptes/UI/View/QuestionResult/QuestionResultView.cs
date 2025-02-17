﻿using TMPro;
using UI.View.Answer;
using UnityEngine;

namespace UI.View.QuestionResult
{
    public class QuestionResultView : MonoBehaviour
    {
        [SerializeField] private TMP_Text questionText;
        [Tooltip("Must be 3")]
        [SerializeField] private AnswerView[] _answerViews;
        
        public static QuestionResultView Create(QuestionResultViewOptions options, QuestionResultView test)
        {
            // ToDo ObjectPool
            var instance = Instantiate(test);
            instance.SetData(options);

            return instance;
        }

        private void SetData(QuestionResultViewOptions options)
        {
            var answers = options.QuestionData.Answers;
            var answerNumber = options.AnswerNumber;
            var correctOptions = options.CorrectOptions;
            var incorrectOptions = options.IncorrectOptions;

            questionText.text = $"{options.QuestionNumber + 1} {options.QuestionData.QuestionText}";
            var i = 0;
            foreach (var answer in answers)
            {
                var view = _answerViews[i];
                view.AnswerText = answer.Text;
                view.SetFrame(i == answerNumber);
                view.NumberText = (i + 1).ToString();
                SetAnswerColor(view, answer.IsCorrect ? correctOptions : incorrectOptions);
                ++i;
            }
        }
        
        private void SetAnswerColor(AnswerView view, in AnswerViewOptions options) 
            => view.SetColor(options.NumberColor, options.AnswerColor);
    }
}