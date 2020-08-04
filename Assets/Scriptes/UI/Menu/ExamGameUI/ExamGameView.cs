using System;
using System.Collections.Generic;
using Core.GameModes.ExamMode;
using Core.Scenes;
using DG.Tweening;
using Subsystem.Question;
using TMPro;
using UI.Menu.ExamGameCompleteUI;
using UI.View;
using UI.View.Answer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu.ExamGameUI
{
    public class ExamGameView : View<ExamGameViewOptions>
    {
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private Button _backButton;
        [SerializeField] private AnswerButtonView[] _buttons;
        [SerializeField] private RectTransform _topPanel;
        [SerializeField] private RectTransform _bottomPanel;
        [SerializeField] private RectTransform _counterTran;
        [SerializeField] private float _stateChangeDuration = 0.5f;
        [SerializeField] private AnswerViewOptions _defaultOptions;
        [SerializeField] private AnswerViewOptions _correctOptions;
        [SerializeField] private AnswerViewOptions _incorrectOptions;

        private ExamGame _game;
        private IGameScene _scene;
        
        private float _topCloseY;
        private float _bottomCloseY;
        private float _couterCloseY;

        private QuestionData _questionData;
        private bool _isClicked;
        private bool isCreate;

        protected override void OnCreate()
        {
            isCreate = true;
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i].Button;
                var temp = i;
                button.onClick.AddListener(() => OnButtonClicked(temp));
            }
            
            _backButton.onClick.AddListener(OnBackButtonClicked);
            base.OnCreate();
        }

        protected override void OnOpenStart(ExamGameViewOptions options)
        {
            if (!isCreate)
            {
                isCreate = true;
                _topCloseY = _topPanel.anchoredPosition.y;
                _bottomCloseY = _bottomPanel.anchoredPosition.y;
                _couterCloseY = _counterTran.anchoredPosition.y;
            }
            _game = options.Game;
            _game.NeedShowQuestion += OnNeedShowQuestion;
            _game.NeedHideQuestion += OnNeedHideQuestion;
            _game.GameOvered += OnGameOvered;
            
            _scene = options.Scene;
            
            _resultText.text = $"{_game.AnswersCount}/{_game.QuestionsCount}";
            _counterTran.DOAnchorPosY(0f, _stateChangeDuration);
            
            _topPanel.DOAnchorPosY(_topCloseY, 0);
            _bottomPanel.DOAnchorPosY(_bottomCloseY, 0);
            _counterTran.DOAnchorPosY(0f, 0);
            
            base.OnOpenStart();
        }

        private void OnGameOvered(IReadOnlyCollection<(QuestionData, int, int)> result)
        {
            var options = new ExamGameCompleteViewOptions(result, _scene);
            ViewManager.OpenExamGameCompleteView(options);
        }


        private void UpdateQuestion(QuestionData question)
        {
            _questionData = question;
            _questionText.text = question.QuestionText;
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var view = _buttons[i].View;
                view.AnswerText = question.Answers[i].Text;
                view.NumberText = (i + 1).ToString();
                view.SetColor(_defaultOptions.NumberColor, _defaultOptions.AnswerColor);
                view.SetFrame(false);
            }
            _isClicked = false;
        }

        private void OnNeedShowQuestion(QuestionData question)
        {
            UpdateQuestion(question);
            _topPanel.DOAnchorPosY(0f, _stateChangeDuration);
            _bottomPanel.DOAnchorPosY(0f, _stateChangeDuration);
            _counterTran.DOAnchorPosY(_couterCloseY, _stateChangeDuration);
        }

        private void OnNeedHideQuestion()
        {
            _topPanel.DOAnchorPosY(_topCloseY, _stateChangeDuration);
            _bottomPanel.DOAnchorPosY(_bottomCloseY, _stateChangeDuration);
            _counterTran.DOAnchorPosY(0f, _stateChangeDuration);
        }

        private void OnButtonClicked(int id)
        {
            if (_isClicked) 
                return;
        
            _isClicked = true;
            _buttons[id].View.SetFrame(true);
            _game.QuestionResolve(id);
            for (var i = 0; i < _buttons.Length; ++i)
            {
                var button = _buttons[i];
                Color number;
                Color answer;
                if (_questionData.Answers[i].IsCorrect)
                {
                    number = _correctOptions.NumberColor;
                    answer = _correctOptions.AnswerColor;
                }
                else
                {
                    number = _incorrectOptions.NumberColor;
                    answer = _incorrectOptions.AnswerColor;
                }
                
                button.View.SetColor(number, answer);
            }

            _resultText.text = $"{_game.AnswersCount}/{_game.QuestionsCount}";
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