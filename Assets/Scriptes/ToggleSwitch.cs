using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;    

public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool _isOn = false;
    public bool isOn => _isOn;

    [SerializeField] private RectTransform toggleIndicator;
    [SerializeField] private Image backgroundImage;

    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    private float offX;
    private float onX;
    [SerializeField] private float tweenTime = 0.25f;

    //private AudioSource audioSource;

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;

    private void Start()
    {
        offX = toggleIndicator.anchoredPosition.x;
        Debug.Log(offX);
        onX = -offX;
        Debug.Log(onX);
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
            toggleIndicator.DOAnchorPosX(onX, tweenTime);
        else
            toggleIndicator.DOAnchorPosX(offX, tweenTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Toogle(!isOn);
    }
}