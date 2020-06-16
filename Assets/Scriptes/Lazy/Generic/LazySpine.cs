#if SPINE_TK2D
using System;
using System.Collections.Generic;
using Spine;
namespace Lazy.Generic
{
    public class LazySpine : BaseLazy
    {
        private readonly Func<TrackEntry> _spineAnimation;
        private readonly Dictionary<string, Queue<Action>> _actions = new Dictionary<string, Queue<Action>>();
        public LazySpine(Func<TrackEntry> spineAnimation)
        {
            _spineAnimation = spineAnimation;
        }

        public LazySpine Listener(string eventName, Action action)
        {
            if (!_actions.ContainsKey(eventName))
                _actions[eventName] = new Queue<Action>();
            _actions[eventName].Enqueue(action);
            return this;
        }
        
        protected override void Execute()
        {
            var entry = _spineAnimation();
            
            entry.Event += (t, e) =>
            {
                var name = e.Data.Name;
                if (_actions.ContainsKey(name))
                    foreach (var a in _actions[name])
                        a();
            };
            
            entry.Complete += _ => Complete();
        }
    }
}
#endif