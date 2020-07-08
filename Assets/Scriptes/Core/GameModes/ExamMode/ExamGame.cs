using System;
using System.Collections.Generic;
using Subsystem.Question;
using UI.Menu.ExamGameCompleteUI;
using UnityEngine;

namespace Core.GameModes.ExamMode
{
    public class ExamGame : Game
    {
        public event Action<QuestionData> NeedShowQuestion;
        public event Action NeedHideQuestion;

        public event Action<IReadOnlyCollection<(QuestionData, int)>> GameOvered;

        private readonly IReadOnlyDictionary<int, QuestionData> _questions;
        private readonly Dictionary<int, ExamObjectState> _states = new Dictionary<int, ExamObjectState>();

        private readonly List<(QuestionData, int)> _result = new List<(QuestionData, int)>();
        private int? _currentId;
        
        public int QuestionsCount => _questions.Count;
        public int AnswersCount => _result.Count;
        public int CorrectAnswersCount { get; private set; }
        public int IncorrectAnswersCount => AnswersCount - CorrectAnswersCount;
        
        public ExamGame(ExamGameOptions options) : base(options)
        {
            _questions = options.Questions;
            foreach (var id in Ids)
            {
                var state = new ExamObjectState(_questions.ContainsKey(id));
                var temp = id;
                state.NeedShowQuestion += () => OnNeedShowQuestion(temp);
                state.NeedHideQuestion += () => OnNeedHideQuestion(temp);
                _states.Add(id, state);
            }
        }

        public override void OnObjectFocus(int id)
        {
            if (!_states.ContainsKey(id))
                return;

            _currentId = id;
            _states[id].OnObjectFocus();
            base.OnObjectFocus(id);

        }

        public override void OnObjectDefocus(int id)
        {
            if (!_states.ContainsKey(id))
                return;

            _states[id].OnObjectDefocus();
            base.OnObjectDefocus(id);
        }

        public void QuestionResolve(int answerNumber)
        {
            if (_currentId == null)
            {
                Debug.LogError(
                    $"{nameof(ExamGame)}.{nameof(QuestionResolve)} must be invoke, when a question active!");
                return;
            }

            var questionId = _currentId.Value;
            var question = _questions[questionId];
            _result.Add((question, answerNumber));
            if (question.Answers[answerNumber].IsCorrect)
                ++CorrectAnswersCount;
            
            _states[questionId].QuestionResolve();

            if (AnswersCount >= QuestionsCount)
                GameOvered?.Invoke(_result);
        }

        private void OnNeedShowQuestion(int id)
        {
            NeedShowQuestion?.Invoke(_questions[id]);
        }

        private void OnNeedHideQuestion(int id)
        {
            NeedHideQuestion?.Invoke();
            _currentId = null;
        }
    }
}