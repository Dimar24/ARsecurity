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
        [SerializeField] private Text _titleText;
        // ToDo Сделать класс с констатами
        [SerializeField] private AnswerViewOptions _correctOptions;
        [SerializeField] private AnswerViewOptions _incorrectOptions;
        [SerializeField] private string _titleFormat = "Правильных ответов: {0} из {1}";

        protected override void OnCreate()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
            base.OnCreate();
        }
        
        protected override void OnOpenStart(ExamGameCompleteViewOptions options)
        {
            var results = options.Results;
            var correctAnswerCount = 0;
            // ToDo удалить это
            for (int ttt = 0; ttt < 2; ttt++)
            {
                foreach (var (question, answerNumber) in results)
                {
                    var resultViewOptions = new QuestionResultViewOptions(
                        question.Answers, 
                        answerNumber,
                        _correctOptions, 
                        _incorrectOptions);
                
                    var resultView = QuestionResultView.Create(resultViewOptions, _prefab);
                    if (question.Answers[answerNumber].IsCorrect)
                        ++correctAnswerCount;
                    resultView.transform.SetParent(_listView.content);
                }
            }
            

            _titleText.text = string.Format(_titleFormat, correctAnswerCount.ToString(), results.Count.ToString());
            base.OnOpenStart(options);
        }
        
        private void OnBackButtonClicked()
        {
            ViewManager.OpenMainView();
        }
    }
}

