using System;
using System.Threading.Tasks;

namespace Lazy.Generic
{
    public class LazyTask : BaseLazy
    {
        private readonly Func<Task> _task;

        public LazyTask(Func<Task> task)
        {
            _task = task;
        }
        
        protected override async void Execute()
        {
            await _task();
            Complete();
        }
    }
}
