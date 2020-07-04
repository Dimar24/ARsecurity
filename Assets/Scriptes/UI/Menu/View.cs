using System;
using Lazy;

namespace UI.Menu
{
    public class View : BaseView
    {
        protected override LazySequence GetOpenLazySequence =>
            new LazySequence()
                .Append(OnOpenStart)
                .Append(OnOpenAnimation)
                .Append(OnOpenComplete);

        protected override LazySequence GetCloseLazySequence =>
            new LazySequence()
                .Append(OnCloseStart)
                .Append(OnCloseAnimation)
                .Append(OnCloseComplete);

        protected virtual void OnOpenStart() { }

        protected virtual void OnOpenAnimation(Action complete)
        {
            gameObject.SetActive(true);
            complete?.Invoke();
        }

        protected virtual void OnOpenComplete()
        {
        }

        protected virtual void OnCloseStart()
        {
        }
        
        protected virtual void OnCloseAnimation(Action complete)
        {
            gameObject.SetActive(false);
            complete?.Invoke();
        }
        
        protected virtual void OnCloseComplete()
        {
        }
    }
    
    public class View<T> : View
    {
        protected override LazySequence GetOpenLazySequence =>
            new LazySequence()
                .Append(() => OnOpenStart((T)ObjectParam))
                .Append(OnOpenAnimation)
                .Append(OnOpenComplete);

        protected virtual void OnOpenStart(T param) { }

        protected sealed override void OnOpenStart() { }
    }
}