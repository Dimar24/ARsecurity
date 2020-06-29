using System;
using System.Collections.Generic;
using Subsystem.Question;

namespace Core.GameModes.ExamMode
{
    public class ExamGame : Game
    {
        public event Action<QuestionData> NeedShowQuestion;
        public event Action NeedHideQuestion;
        public event Action NeedShowParticles;
        public event Action NeedHideParticles;
        
        private readonly IReadOnlyDictionary<int, QuestionData> _questions;
        private readonly Dictionary<int, ExamObjectState> _states = new Dictionary<int, ExamObjectState>();

        private int? _currentId;

        public ExamGame(ExamGameOptions options) : base(options)
        {
            _questions = options.Questions;
            foreach (var id in Ids)
            {
                var state = new ExamObjectState(_questions.ContainsKey(id));
                var temp = id;
                state.NeedShowParticles += () => OnNeedShowParticles(temp);
                state.NeedHideParticles += () => OnNeedHideParticles(temp);
                state.NeedShowQuestion += () => OnNeedShowQuestion(temp);
                state.NeedHideQuestion += () => OnNeedHideQuestion(temp);
                _states.Add(id, state);
            }
        }

        public override void OnObjectFocus(int id)
        {
            if (!_states.ContainsKey(id))
                return;
            
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

        private void OnNeedShowQuestion(int id)
        {
            NeedShowQuestion?.Invoke(_questions[id]);
        }

        private void OnNeedHideQuestion(int id)
        {
            NeedHideQuestion?.Invoke();
        }
        
        private void OnNeedShowParticles(int id)
        {
            NeedShowParticles?.Invoke();
        }
        private void OnNeedHideParticles(int id)
        {
            NeedHideParticles?.Invoke();
        }
    }
}