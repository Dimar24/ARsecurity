using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Core.GameModes.ExamMode
{
    public class ExamObjectState
    {
        public event Action NeedShowQuestion;
        public event Action NeedHideQuestion;
        
        
        private bool _isNeedShowQuestion;
        private bool _isQuestionShowed;
        
        private Coroutine _showCoroutine;
        private Coroutine _hideCoroutine;

        public ExamObjectState(bool needShowQuestion)
        {
            _isNeedShowQuestion = needShowQuestion;
        }
        
        public void OnObjectFocus()
        {
            if (_showCoroutine != null)
                return;

            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
                _hideCoroutine = null;
            }

            _showCoroutine = StartCoroutine(ShowObjectRoutine(() => _showCoroutine = null));
        }

        public void OnObjectDefocus()
        {
            if (_hideCoroutine != null)
                return;

            if (_showCoroutine != null)
            {
                StopCoroutine(_showCoroutine);
                _showCoroutine = null;
            }
            _hideCoroutine = StartCoroutine(HideObjectRoutine(() => _hideCoroutine = null));
        }

        public void QuestionResolve()
        {
            StartCoroutine(QuestionResolveRoutine());
        }

        private IEnumerator QuestionResolveRoutine(Action complete = null)
        {
            _isNeedShowQuestion = false;
            yield return new WaitForSeconds(1f);
            if (_isQuestionShowed)
            {
                NeedHideQuestion?.Invoke();
                _isQuestionShowed = false;
            }
            complete?.Invoke();
        }
        
        private IEnumerator ShowObjectRoutine(Action complete = null)
        {
            if (!_isNeedShowQuestion)
                yield break;
            
            yield return new WaitForSeconds(2f);

            if (!_isQuestionShowed)
            {
                NeedShowQuestion?.Invoke();
                _isQuestionShowed = true;
            }
            
            complete?.Invoke();
        }
        
        private IEnumerator HideObjectRoutine(Action complete = null)
        {
            yield return new WaitForSeconds(3f);
            
            if (_isQuestionShowed)
            {
                NeedHideQuestion?.Invoke();
                _isQuestionShowed = false;
            }
            
            complete?.Invoke();
        }
        
        private Coroutine StartCoroutine(IEnumerator routine) => MainThreadHelper.StartCoroutineMainThread(routine);
        private void StopCoroutine(Coroutine coroutine) => MainThreadHelper.StopCoroutineMainThread(coroutine);
    }
}