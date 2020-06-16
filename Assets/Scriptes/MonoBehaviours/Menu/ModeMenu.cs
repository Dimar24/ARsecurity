using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene(1);
    }
    
    private void OnBackButtonClicked()
    {
        MenuManager.ModeMenu.SetActive(false);
        MenuManager.MainMenu.SetActive(true);
    }
}
