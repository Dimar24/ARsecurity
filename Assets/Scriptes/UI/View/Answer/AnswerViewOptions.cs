using System;
using UnityEngine;

namespace UI.View.Answer
{
    [Serializable]
    public struct AnswerViewOptions
    {
        [SerializeField] private Color _numberColor;
        [SerializeField] private Color _answerColor;

        public Color NumberColor => _numberColor;
        public Color AnswerColor => _answerColor;
    }
}