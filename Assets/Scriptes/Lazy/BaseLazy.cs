using System;

namespace Lazy
{
    public abstract class BaseLazy
    {
        /// <summary>
        /// Вызывается в начале выполнения контейнера.
        /// </summary>
        public event Action Started;
        
        /// <summary>
        /// Вызывается в конце выполнения контейнера.
        /// </summary>
        public event Action Completed;

        /// <summary>
        /// Подписывает Action на конец выполнения контейнера.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>this</returns>
        public virtual BaseLazy OnStart(Action action)
        {
            Started += action;
            return this;
        }
        
        /// <summary>
        /// Подписывает Action на конец выполнения контейнера.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>this</returns>
        public virtual BaseLazy OnComplete(Action action)
        {
            Completed += action;
            return this;
        }
        
        /// <summary>
        /// Запускает выполнение контейнера.
        /// </summary>
        public void Run()
        {
            Started?.Invoke();
            Execute();
        }

        protected void Complete() =>  Completed?.Invoke();
        
        protected virtual void Execute() => Complete();
    }
}
