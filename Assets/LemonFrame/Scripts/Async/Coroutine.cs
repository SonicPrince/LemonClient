using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lemon
{
    public sealed class Coroutine : AsyncOp
    {
        private IEnumerator _routine;

        public Coroutine(IEnumerator routine)
        {
            _routine = routine;
            Completed = (routine == null);
        }

        public bool Completed { get; private set; }

        public bool MoveNext()
        {
            if (_routine == null)
                return false;

            object current = _routine.Current;

            if(current is IWait)
            {
                IWait wait = (IWait)current;
                //检测等待条件，条件满足，跳到迭代器的下一元素 （IEnumerator方法里的下一个yield）
                if (!wait.Tick())
                {
                    return true;
                }
            }

            if (current != null)
            {
                if (!AsyncCompleteChecker.IsCompleted(current))
                    return true;
            }

            Completed = !_routine.MoveNext();

            return !Completed;
        }

        public void Stop()
        {
            _routine = null;
            Completed = true;
        }
    }
}

