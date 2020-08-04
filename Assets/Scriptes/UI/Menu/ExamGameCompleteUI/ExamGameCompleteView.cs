using Core.Scenes;
using TMPro;
using UI.View.Answer;
using UI.View.QuestionResult;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu.ExamGameCompleteUI
{
    public class ExamGameCompleteView : View<ExamGameCompleteViewOptions>
    {
        [SerializeField] private ScrollRect _listView;
        // ToDo ObjectPool
        [SerializeField] private QuestionResultView _prefab;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _titleText;
        // ToDo Сделать класс с констатами
        [SerializeField] private AnswerViewOptions _correctOptions;
        [SerializeField] private AnswerViewOptions _incorrectOptions;
        [SerializeField] private string _titleFormat = "Правильных ответов: {0} из {1}";

        private IGameScene _scene;
        
        protected override void OnCreate()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            base.OnCreate();
        }
        
        protected override void OnOpenStart(ExamGameCompleteViewOptions options)
        {
            _scene = options.Scene;
            var results = options.Results;
            var correctAnswerCount = 0;
            
            foreach (var (question, answerNumber, questionNumber) in results)
            {
                var resultViewOptions = new QuestionResultViewOptions(
                    question, 
                    questionNumber,
                    answerNumber,
                    _correctOptions, 
                    _incorrectOptions);
            
                var resultView = QuestionResultView.Create(resultViewOptions, _prefab);
                if (question.Answers[answerNumber].IsCorrect)
                    ++correctAnswerCount;
                resultView.transform.SetParent(_listView.content);
                resultView.transform.localScale = Vector3.one;
            }
            

            _titleText.text = string.Format(_titleFormat, correctAnswerCount.ToString(), results.Count.ToString());
            base.OnOpenStart(options);
        }
        
        private void OnBackButtonClicked()
        {
            _scene.Unload(() =>
            {
                ViewManager.OpenModeView();
            });
        }
    }
}

