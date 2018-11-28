using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lemon
{
    public interface AsyncOp
    {
        bool Completed { get; }
    }

    public interface AsyncOpEx : AsyncOp
    {
        float Progress { get; }
    }

    public interface AsyncRequest<T> : AsyncOp
    {
        T Result { get; }
    }

    public interface AsyncRequestEx<T> : AsyncRequest<T>, AsyncOpEx
    {
    }

    public class AsyncObserver : AsyncOp
    {
        public delegate bool TargetChecker();

        private TargetChecker _target;
        private bool _inverse;

        public AsyncObserver(TargetChecker target, bool inverse = false)
        {
            _target = target;
            _inverse = inverse;
        }

        public bool Completed
        {
            get { return !_inverse ? _target() : !_target(); }
        }
    }

    public class AsyncParallel : AsyncOp
    {
        private AsyncOp[] _operations;

        public AsyncParallel(params AsyncOp[] operations)
        {
            _operations = operations;
        }

        public bool Completed
        {
            get
            {
                for (int i = 0; i < _operations.Length; ++i)
                {
                    if (!_operations[i].Completed)
                        return false;
                }

                return true;
            }
        }
    }
}

