using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lemon
{
    public class Log
    {
        public static void Error(string msg)
        {
#if UNITY_EDITOR
            Debug.LogError(msg);
#endif
        }

        public static void Info(string msg)
        {
#if UNITY_EDITOR
            Debug.Log(msg);
#endif
        }

    }
}
