using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CilInterpreter
{
    class Input
    {
        private void OnWaitInput()
        {
            WaitInput();
        }

        public event Action WaitInput;
        private ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

        public void AddInput(string data)
        {
            _queue.Enqueue(data);
        }

        public void Stop()
        {
            _stopped = true;
        }

        private bool _stopped;

        public string GetInput()
        {
            if (_queue.Count == 0)
                OnWaitInput();
            string result;
            while (!_queue.TryDequeue(out result) && !_stopped)
                Thread.Sleep(100);
            _stopped = false;
            return result;
        }
    }
}
