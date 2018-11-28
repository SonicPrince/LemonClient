using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lemon
{
    public class Log
    {
        public static void Error(string msg)
        {
            Debug.LogError(msg);
        }

        public static void Info(string msg)
        {
            Debug.Log(msg);
        }

    }
}
