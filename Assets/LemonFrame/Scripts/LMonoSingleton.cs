/***
 * 
 * Author:王飞飞
 * Date:2017-12-30
 * Email:wff_1994@126.com
 * QQ:2062930374
 * 
 * 
 * **/

using System;
using System.Reflection;
using UnityEngine;

namespace Lemon
{
    public abstract class LMonoSingleton<T> : MonoBehaviour where T : LMonoSingleton<T>
    {
        protected static T instance = null;
        protected LMonoSingleton()
        {

        }

        public static T Instance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (FindObjectsOfType<T>().Length > 1)
                {
                    Log.Error("the type more than 1");
                    return instance;
                }
                if (instance == null)
                {
                    string instanceName = typeof(T).Name;
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                        instanceGO = new GameObject(instanceName);
                    instance = instanceGO.AddComponent<T>();
                    DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                    Log.Info("Add New Singleton " + instance.name + " in Game!");
                }
                else
                {
                    Log.Error("Already exist: " + instance.name);
                }

            }

            return instance;
        }

    }


}

