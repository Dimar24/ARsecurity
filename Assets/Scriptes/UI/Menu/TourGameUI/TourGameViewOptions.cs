using Core.GameModes.TourMode;
using Core.Scenes;

namespace UI.Menu.TourGameUI
{
    public class TourGameViewOptions : GameViewOptions
    {
        public readonly TourGame Game;
        
        public TourGameViewOptions(TourGame game, IGameScene scene) : base(scene)
        {
            Game = game;
        }
    }
}