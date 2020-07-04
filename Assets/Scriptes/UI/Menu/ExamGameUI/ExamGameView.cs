using Core;
using Core.GameModes.ExamMode;
using Core.Scenes;
using DG.Tweening;
using Subsystem.Question;
using UI.View.AnswerButton;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu.ExamGameUI
{
    public class ExamGameView : View<ExamGameViewOptions>
    {
        [SerializeField] private Text _questionText;
        [SerializeField] private Button _backButton;
        [SerializeField] private AnswerButtonView[] _buttons;
        [SerializeField] private RectTransform _topPanel;
        [SerializeField] private RectTransform _bottomPanel;
        [SerializeField] private float _stateChangeDuration = 0.5f;
        [SerializeField] private AnswerButtonOptions _defaultOptions;
        [SerializeField] private AnswerButtonOptions _correctOptions;
        [SerializeField] private AnswerButtonOptions _incorrectOptions;

        private ExamGame _game;
        private IGameScene _scene;
        
        private float _topCloseY;
        private float _bottomCloseY;

        private QuestionData _questionData;
        private bool _isClicked;
        
        protected override void OnCreate()
        {
            _topCloseY = _topPanel.anchoredPosition.y;
            _bottomCloseY = _bottomPanel.anchoredPosition.y;
            _backButton.onClick.AddListener(OnBackButtonClicked);
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i];
                var temp = i;
                button.Clicked += () => OnButtonClicked(temp);
            }
            base.OnCreate();
        }

        protected override void OnOpenStart(ExamGameViewOptions options)
        {
            _game = options.Game;
            _game.NeedShowQuestion += OnNeedShowQuestion;
            _game.NeedHideQuestion += OnNeedHideQuestion;

            _scene = options.Scene;
            base.OnOpenStart();
        }
    

        private void UpdateQuestion(QuestionData question)
        {
            _questionData = question;
            _questionText.text = question.QuestionText;
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i];
                button.AnswerText = question.Answers[i].Text;
                button.NumberText = (i + 1).ToString();
                button.SetColor(_defaultOptions.NumberColor, _defaultOptions.AnswerColor);
                button.SetFrame(false);
            }
            _isClicked = false;
        }

        private void OnNeedShowQuestion(QuestionData question)
        {
            Debug.Log("OnNeedShowQuestion UI");
            UpdateQuestion(question);
            _topPanel.DOAnchorPosY(0f, _stateChangeDuration);
            _bottomPanel.DOAnchorPosY(0f, _stateChangeDuration);
        }

        private void OnNeedHideQuestion()
        {
            Debug.Log("OnNeedHideQuestion UI");
            _topPanel.DOAnchorPosY(_topCloseY, _stateChangeDuration);
            _bottomPanel.DOAnchorPosY(_bottomCloseY, _stateChangeDuration);
        }

        private void OnButtonClicked(int id)
        {
            if (_isClicked) 
                return;
        
            _isClicked = true;
            _buttons[id].SetFrame(true);
            _game.QuestionResolve(_questionData.Answers[id].IsCorrect);
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i];
                if (_questionData.Answers[i].IsCorrect)
                    button.SetColor(_correctOptions.NumberColor, _correctOptions.AnswerColor);
                else
                    button.SetColor(_incorrectOptions.NumberColor, _incorrectOptions.AnswerColor);
            }
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