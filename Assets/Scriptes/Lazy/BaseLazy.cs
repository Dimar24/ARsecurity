using System;
using UnityEngine;

namespace Lazy
{
    public abstract class BaseLazy
    {
        protected const string ModifyAfterStartLog = "Do not modify the container after starting it!";
        protected static readonly string RunAfterStartLog = $"Do not use {nameof(Run)} the container after starting it!";

        protected bool IsRun;

        /// <summary>
        /// Вызывается в начале выполнения контейнера.
        /// </summary>
        public event Action Started;
        
        /// <summary>
        /// Вызывается в конце выполнения контейнера.
        /// </summary>
        public event Action Completed;

        /// <summary>
        /// Подписывает Action на старт выполнения контейнера.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>this</returns>
        public virtual BaseLazy OnStart(Action action)
        {
            if (IsRun)
            {
                Debug.LogWarning(ModifyAfterStartLog);
                return this;
            }
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
            if (IsRun)
            {
                Debug.LogWarning(ModifyAfterStartLog);
                return this;
            }
            
            Completed += action;
            return this;
        }
        
        /// <summary>
        /// Запускает выполнение контейнера.
        /// </summary>
        public void Run()
        {
            if (IsRun)
            {
                Debug.LogWarning(RunAfterStartLog);
                return;
            }

            IsRun = true;
            Started?.Invoke();
            Execute();
        }

        protected void Complete() =>  Completed?.Invoke();
        
        protected virtual void Execute() => Complete();
    }
}
