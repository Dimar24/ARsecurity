using System.Collections.Generic;

namespace Lazy
{
    public sealed class LazySequence : BaseLazy
    {
        private readonly List<Queue<BaseLazy>> _list = new List<Queue<BaseLazy>>();
        private int _pointer;
        
        /// <summary>
        /// Добавляет контейнер асинхронно.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>this</returns>
        public LazySequence Join(BaseLazy container)
        {
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
            if (_list.Count <= 0 || _list[_list.Count - 1].Count > 0)
                _list.Add(new Queue<BaseLazy>());
            
            return this;
        }

        public void Break()
        {
            _pointer = _list.Count;
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
            
            var count = queue.Count;
            var pointer = 0;

            void TryComplete()
            {
                ++pointer;
                if (pointer >= count)
                    Execute();
            }
            
            foreach (var container in queue)
                container.OnComplete(TryComplete).Run();
        }
    }
}
