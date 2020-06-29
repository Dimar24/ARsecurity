
using System.Collections.Generic;

namespace Core.GameModes
{
    public class GameOptions
    {
        public readonly HashSet<int> Ids;
        
        public GameOptions(HashSet<int> ids)
        {
            Ids = ids;
        }
    }
}