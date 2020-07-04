using System;
using System.Collections.Generic;
using Subsystem.Question;
using UnityEngine;

namespace Core.GameModes.ExamMode
{
    public class ExamGame : Game
    {
        public event Action<QuestionData> NeedShowQuestion;
        public event Action NeedHideQuestion;
        
        private readonly IReadOnlyDictionary<int, QuestionData> _questions;
        private readonly Dictionary<int, ExamObjectState> _states = new Dictionary<int, ExamObjectState>();

        private int? _currentId;

        public int QuestionsCount => _questions.Count;
        public int AnswersCount { get; private set; }
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

            _currentId = null;
            _states[id].OnObjectDefocus();
            base.OnObjectDefocus(id);
        }

        public void QuestionResolve(bool isCorrect)
        {
            if (_currentId == null)
            {
                Debug.LogError(
                    $"{nameof(ExamGame)}.{nameof(QuestionResolve)} must be invoke, when a question active!");
                return;
            }

            ++AnswersCount;
            if (isCorrect)
                ++CorrectAnswersCount;
            
            _states[_currentId.Value].QuestionResolve();
        }
        
        private void OnNeedShowQuestion(int id)
        {
            NeedShowQuestion?.Invoke(_questions[id]);
        }

        private void OnNeedHideQuestion(int id)
        {
            NeedHideQuestion?.Invoke();
        }
    }
}