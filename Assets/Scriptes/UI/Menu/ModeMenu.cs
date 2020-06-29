using Core;
using UnityEngine;
using UnityEngine.UI;

public class ModeMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        GameManager.Load(new SessionOptions(GameModeType.Exam), () =>
        {
            MenuManager.ModeMenu.SetActive(false);
            MenuManager.GameMenu.SetActive(true);
        });
    }
    
    private void OnBackButtonClicked()
    {
        MenuManager.ModeMenu.SetActive(false);
        MenuManager.MainMenu.SetActive(true);
    }
}
