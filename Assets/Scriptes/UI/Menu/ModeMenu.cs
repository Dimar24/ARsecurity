using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ModeMenu : BaseMenu
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _backButton;

        protected override void OnCreate()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _backButton.onClick.AddListener(OnBackButtonClicked);
            base.OnCreate();
        }

        private void OnPlayButtonClicked()
        {
            GameManager.Load(new SessionOptions(GameModeType.Exam), () =>
            {
                MenuManager.Open<ExamGameMenu>();
            });
        }
    
        private void OnBackButtonClicked()
        {
            MenuManager.Open<MainMenu>();
        }
    }
}
