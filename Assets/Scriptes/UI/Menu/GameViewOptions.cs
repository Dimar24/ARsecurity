using Core.Scenes;

namespace UI.Menu
{
    public class GameViewOptions
    {
        public readonly IGameScene Scene;

        public GameViewOptions(IGameScene scene)
        {
            Scene = scene;
        }
    }
}