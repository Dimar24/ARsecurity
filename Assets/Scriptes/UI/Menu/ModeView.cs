using Core;
using Core.Scenes;
using UI.Menu.ExamGameUI;
using UI.Menu.TourGameUI;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ModeView : View
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private ToggleSwitch _toggle;

        private bool _isClicked;
        
        protected override void OnCreate()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _backButton.onClick.AddListener(OnBackButtonClicked);
            base.OnCreate();
        }

        protected override void OnOpenStart()
        {
            _isClicked = false;
            base.OnOpenStart();
        }
        
        private void OnPlayButtonClicked()
        {
            if (_isClicked)
                return;
            _isClicked = true;
            
            if (_toggle.Value)
                OpenTourGame();
            else
                OpenExamGame();
        }
        
        private void OpenExamGame()
        {
            var game = GameBuilder.CreateExamGame();
            GameScene.Load(game, scene =>
            {
                var options = new ExamGameViewOptions(game, scene);
                ViewManager.OpenExamGameView(options);
            });
        }
        

        private void OpenTourGame()
        {
            var game = GameBuilder.CreateTourGame();
            GameScene.Load(game, scene =>
            {
                var options = new TourGameViewOptions(game, scene);
                ViewManager.OpenTourGameView(options);
            });
        }

        
        private void OnBackButtonClicked()
        {
            ViewManager.OpenMainView();
        }
    }
}
