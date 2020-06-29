using Subsystem.Question;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private int _number = 10;
    [SerializeField] private float _durationReset = 1.0f;
    
    private int _count;
    private float _lastClicked;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        _count = 0;
        MenuManager.MainMenu.SetActive(false);
        MenuManager.ModeMenu.SetActive(true);
    }
    
    private void OnExitButtonClicked()
    {
        if (!MenuManager.ExitPopup.activeSelf)
        {
            if (Time.time - _lastClicked > _durationReset)
                _count = 0;
            _count++;
            if (_count == _number)
            {
                _count = 0;
                MenuManager.ExitPopup.SetActive(true);
            }
            _lastClicked = Time.time;   
        }
    }
}
