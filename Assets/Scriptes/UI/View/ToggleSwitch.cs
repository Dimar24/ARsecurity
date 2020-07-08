using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.View
{
    public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private Image _background;
        [SerializeField] private bool _value = false;
        [SerializeField] private float _changDuration = 0.25f;
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;

        private float _offX;
        private float _onX;

        public bool Value => _value;
        
        public event Action<bool> Changed;

        private void Start()
        {
            _offX = _indicator.anchoredPosition.x;
            _onX = -_offX;
        }

        private void OnEnable()
        {
            Toogle(_value);
        }

        private void Toogle(bool value)
        {
            if (_value == value) 
                return;
            
            _value = value;
            _background.DOColor(value ? _onColor : _offColor, _changDuration);
            _indicator.DOAnchorPosX(value ? _onX : _offX, _changDuration);
            Changed?.Invoke(value);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Toogle(!_value);
        }
    }
}