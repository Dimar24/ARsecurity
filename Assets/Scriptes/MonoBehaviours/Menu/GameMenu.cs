using System;
using System.Collections;
using Core;
using Core.GameModes.ExamMode;
using DG.Tweening;
using Subsystem.Question;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Text _questionText;
    [SerializeField] private AnswerButtonView[] _buttons;
    [SerializeField] private RectTransform _topPanel;
    [SerializeField] private RectTransform _bottomPanel;
    [SerializeField] private float _stateChangeDuration = 0.5f;
    private ExamGame _game;
    private float _topCloseY;
    private float _bottomCloseY;
    
    private void Awake()
    {
        _topCloseY = _topPanel.anchoredPosition.y;
        _bottomCloseY = _bottomPanel.anchoredPosition.y;
    }

    private void OnEnable()
    {
        _game = GameManager.Game as ExamGame;
        if (_game == null)
        {
            Debug.LogError($"Game must be {nameof(ExamGame)}");
            return;
        }

        _game.NeedShowQuestion += OnNeedShowQuestion;
        _game.NeedHideQuestion += OnNeedHideQuestion;
        _game.NeedShowParticles += OnNeedShowParticles;
        _game.NeedHideParticles += OnNeedHideParticles;
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
    
    private void OnNeedHideParticles()
    {
        Debug.Log("OnNeedHideParticles UI");

    }

    private void OnNeedShowParticles()
    {
        Debug.Log("OnNeedShowParticles UI");
    }

    private void UpdateQuestion(QuestionData question)
    {
        _questionText.text = question.QuestionText;
        foreach (var button in _buttons)
        {
            button.AnswerText = question.QuestionText;
        }
    }
}
