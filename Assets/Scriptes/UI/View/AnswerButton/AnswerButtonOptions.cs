using System;
using UnityEngine;

namespace UI.View.AnswerButton
{
    [Serializable]
    public struct AnswerButtonOptions
    {
        [SerializeField] private Color _numberColor;
        [SerializeField] private Color _answerColor;

        public Color NumberColor => _numberColor;
        public Color AnswerColor => _answerColor;
    }
}