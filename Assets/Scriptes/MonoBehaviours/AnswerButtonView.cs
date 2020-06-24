using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AnswerButtonView : MonoBehaviour
{
    [SerializeField] private Text _numberText;
    [SerializeField] private Text _answerText;
    private Button _button;
    
    [SerializeField] private GameObject _answerFrameImage;
    
    [SerializeField] private Image _numberImage;
    [SerializeField] private Image _answerImage;
    
    public string NumberText
    {
        get => _numberText.text;
        set => _numberText.text = value;
    }

    public string AnswerText
    {
        get => _answerText.text;
        set => _answerText.text = value;
    }

    public event Action Clicked;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Clicked?.Invoke();
    }

    public void SetFrame(bool enabled)
    {
        _answerFrameImage.SetActive(enabled);
    }

    public void SetColor(Color numberColor, Color answerColor)
    {
        _numberImage.color = numberColor;
        _answerImage.color = answerColor;
    }
    
    
}
