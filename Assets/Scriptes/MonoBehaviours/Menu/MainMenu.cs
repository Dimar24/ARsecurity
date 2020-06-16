using Subsystem.Question;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        QuestionManager.LoadQuestionsAsync();
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        MenuManager.MainMenu.SetActive(false);
        MenuManager.ModeMenu.SetActive(true);
    }
    
    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
