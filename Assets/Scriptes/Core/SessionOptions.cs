namespace Core
{
    public class SessionOptions
    {
        public readonly GameModeType GameModeType;

        public SessionOptions(GameModeType gameModeType)
        {
            GameModeType = gameModeType;
        }
    }
}