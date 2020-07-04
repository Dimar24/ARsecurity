using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    public sealed class LazySequence : BaseLazy
    {
        private readonly List<Queue<BaseLazy>> _list = new List<Queue<BaseLazy>>();
        
        private int _pointer;
        private int _stepPointer;

        /// <summary>
        /// Добавляет контейнер асинхронно.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>this</returns>
        public LazySequence Join(BaseLazy container)
        {
            if (IsRun)
            {
                Debug.LogWarning(ModifyAfterStartLog);
                return this;
            }
            
            if (_list.Count <= 0)
                _list.Add(new Queue<BaseLazy>());
            
            _list[_list.Count - 1].Enqueue(container);
            return this;
        }
        
        /// <summary>
        /// Добавляет контейнер синхронно.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>this</returns>
        public LazySequence Append(BaseLazy container)
        {
            if (IsRun)
            {
                Debug.LogWarning(ModifyAfterStartLog);
                return this;
            }
            
            _list.Add(new Queue<BaseLazy>());
            _list[_list.Count - 1].Enqueue(container);
            return this;
        }

        /// <summary>
        /// Добавляет следующий шаг, если это необходимо. 
        /// </summary>
        /// <returns>this</returns>
        public LazySequence Append()
        {
            if (IsRun)
            {
                Debug.LogWarning(ModifyAfterStartLog);
                return this;
            }
            
            if (_list.Count <= 0 || _list[_list.Count - 1].Count > 0)
                _list.Add(new Queue<BaseLazy>());
            
            return this;
        }

        protected override void Execute()
        {
            if (_list.Count <= _pointer)
            {
                _pointer = 0;
                Complete();
                return;
            }
            
            var queue = _list[_pointer];
            ++_pointer;
            _stepPointer = 0;

            foreach (var container in queue)
                container.OnComplete(TryComplete).Run();
        }
        
         private void TryComplete()
        {
            ++_stepPointer;
            if (_stepPointer >= _list[_pointer - 1].Count)
                Execute();
        }
    }
}
