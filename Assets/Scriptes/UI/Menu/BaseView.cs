using System;
using Lazy;
using UnityEngine;

namespace UI.Menu
{
    // ToDo Закомментить и превратить в плагин
    public class BaseView : MonoBehaviour
    {
        protected virtual LazySequence GetOpenLazySequence => new LazySequence();

        protected virtual LazySequence GetCloseLazySequence => new LazySequence();
        
        protected object ObjectParam { get; private set; }
        
        protected void Start()
        {
            OnCreate();
        }

        protected virtual void OnCreate() { }

        public void Open(object param, Action complete = null)
        {
            ObjectParam = param;
            GetOpenLazySequence
                .OnComplete(() =>
                {
                    complete?.Invoke();
                })
                .Run();
        }
        
        public void Close(Action complete = null)
        {
            GetCloseLazySequence
                .OnComplete(() => complete?.Invoke())
                .Run();
        }
    }
}