using System;

namespace Core.Scenes
{
    public interface IGameScene
    {
        void Unload(Action complete = null);
    }
}