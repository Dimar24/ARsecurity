using System;
using System.Collections;
using Lazy;
using Lazy.Generic;
using UnityEngine;

namespace Lazy.Generic
{
    public sealed class LazyWhile : BaseLazy
    {
        private readonly Func<bool> _func;
        private readonly YieldInstruction _yield;
        
        public LazyWhile(Func<bool> func, YieldInstruction yield = null)
        {
            _func = func;
            _yield = yield;
        }

        protected override void Execute() => new LazyCoroutine(Coroutine).OnComplete(Complete).Run();

        private IEnumerator Coroutine()
        {
            while (_func())
                yield return _yield;
        }
    }
}