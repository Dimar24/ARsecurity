using System;
using DG.Tweening;

namespace Lazy.Generic
{
    public class LazyTween : BaseLazy
    {
        private readonly Func<Tween> _tween;

        public LazyTween(Func<Tween> tween)
        {
            _tween = tween;
        }
    
        protected override void Execute()
        {
            _tween().onComplete += Complete;
        }
    }
}