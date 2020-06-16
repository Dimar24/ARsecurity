using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Lazy.Generic
{
    public sealed class LazyCoroutine : BaseLazy
    {
        private readonly Func<IEnumerator> _func1;
        private readonly Func<YieldInstruction> _func2;
        
        public LazyCoroutine(Func<IEnumerator> func) => _func1 = func;

        public LazyCoroutine(Func<YieldInstruction> func) => _func2 = func;
        
        protected override void Execute() => MainThreadHelper.StartCoroutineMainThread(Coroutine());

        private IEnumerator Coroutine()
        {
            if (_func1 != null)
                yield return _func1();
            else
                yield return _func2();
            
            Complete();
        }
    }
    
}