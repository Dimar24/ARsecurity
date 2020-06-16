using System;

namespace Lazy.Generic
{
    public class LazyCallback : BaseLazy
    {
        private readonly Action<Action> _action;
    
        public LazyCallback(Action<Action> action) => _action = action;

        public static implicit operator Action<Action>(LazyCallback action) => action._action;
        
        public static implicit operator LazyCallback(Action<Action> action) => new LazyCallback(action);

        protected override void Execute() => _action(Complete);
    }
}
