using Core.GameModes.ExamMode;
using Core.Scenes;

namespace UI.Menu.ExamGameUI
{
    public class ExamGameViewOptions : GameViewOptions
    {
        public readonly ExamGame Game;

        public ExamGameViewOptions(ExamGame game, IGameScene scene) : base(scene)
        {
            Game = game;
        }
    }
}