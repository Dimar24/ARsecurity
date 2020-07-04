using Core;
using Core.GameModes.TourMode;
using Core.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu.TourGameUI
{
    public class TourGameView : View<TourGameViewOptions>
    {
        [SerializeField] private Button _backButton;

        private TourGame _game;
        private IGameScene _scene;
        
        protected override void OnCreate()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
            base.OnCreate();
        }

        protected override void OnOpenStart(TourGameViewOptions options)
        {
            _game = options.Game;
            _scene = options.Scene;
            base.OnOpenStart();
        }
        
        private void OnBackButtonClick()
        {
            _scene.Unload(() =>
            {
                ViewManager.OpenModeView();
            });
        }
    }
}
