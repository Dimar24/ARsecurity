using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.View
{
    // ToDo Refactoring
    public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private bool _isOn = false;
        public bool isOn => _isOn;

        [SerializeField] private RectTransform toggleIndicator;
        [SerializeField] private Image backgroundImage;

        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;
        [SerializeField] private float tweenTime = 0.25f;

        private float _offX;
        private float _onX;

        public delegate void ValueChanged(bool value);
        public event ValueChanged valueChanged;

        private void Start()
        {
            _offX = toggleIndicator.anchoredPosition.x;
            Debug.Log(_offX);
            _onX = -_offX;
            Debug.Log(_onX);
            //audioSource = this.GetComponent<AudioSource>();
        }

        public void OnEnable()
        {
            Toogle(isOn);
        }

        private void Toogle(bool value, bool playSFX = true)
        {
            if (value != isOn)
            {
                _isOn = value;
                ToggleColor(isOn);
                MoveIndicator(isOn);
            
                //if (playSFX)
                //audioSource.Play();

                valueChanged?.Invoke(isOn);
            }
        }

        private void ToggleColor(bool value)
        {
            if (value)
                backgroundImage.DOColor(onColor, tweenTime);
            else
                backgroundImage.DOColor(offColor, tweenTime);
        }
    
        private void MoveIndicator(bool value)
        {
            if (value)
                toggleIndicator.DOAnchorPosX(_onX, tweenTime);
            else
                toggleIndicator.DOAnchorPosX(_offX, tweenTime);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Toogle(!isOn);
        }
    }
}