using System;

namespace Lazy.Generic
{
    public class LazyAction : BaseLazy
    {
        private readonly Action _action;

        public LazyAction(Action action) => _action = action;

        public static implicit operator Action(LazyAction action) => action.Execute;
        
        public static implicit operator LazyAction(Action action) => new LazyAction(action);

        protected override void Execute()
        {
            _action();
            Complete();
        }
    }
}