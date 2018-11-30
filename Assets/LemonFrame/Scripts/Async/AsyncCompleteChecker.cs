using System.Collections;
using System.Collections.Generic;
using System;

namespace Lemon
{
    public static class AsyncCompleteChecker
    {
        public static bool IsCompleted(object obj)
        {
            if (obj == null)
                return true;

            var op = obj as AsyncOp;
            if (op != null)
                return op.Completed;

            foreach (var checker in _checkers)
            {
                if (!checker(obj))
                    return false;
            }

            return true;
        }

        public static void AddChecker(Func<object, bool> checker)
        {
            if (checker == null)
                return;

            if (_checkers.IndexOf(checker) >= 0)
                return;

            _checkers.Add(checker);
        }

        private static List<Func<object, bool>> _checkers = new List<Func<object, bool>>();

        static AsyncCompleteChecker() { }
    }
}
