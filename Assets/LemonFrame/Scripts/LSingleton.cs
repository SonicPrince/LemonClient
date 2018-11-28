/***
 * 
 * Author:王飞飞
 * Date:2017-12-30
 * Email:wff_1994@126.com
 * QQ:756726530
 * 
 * 
 * **/

using System;
using System.Reflection;

namespace Lemon
{
    public abstract class LSingleton<T> where T : LSingleton<T>
    {
        protected static T instance = null;
        protected LSingleton()
        {

        }

        public virtual void Init()
        {

        }

        public static T Instance()
        {
            if (instance == null)
            {
                ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                //获取无参数的构造函数
                ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                if (ctor != null)
                {
                    instance = ctor.Invoke(null) as T;
                    instance.Init();
                }
                else {
                    throw new Exception("Non-public ctor() not found!");
                }
            }

            return instance;
        }

    }


}

