using System;
using UnityEngine;

[Serializable]
public struct AnswerButtonOptions
{
    [SerializeField] private Color _numberColor;
    [SerializeField] private Color _answerColor;

    public Color NumberColor => _numberColor;
    public Color AnswerColor => _answerColor;
}