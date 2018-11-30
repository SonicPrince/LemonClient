using System;
using System.Collections;
using System.Collections.Generic;

namespace Lemon
{
    public class CoroutineManager
    {
        private static CoroutineManager _instance = null;
        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CoroutineManager();
                }
                return _instance;
            }
        }

        private LinkedList<Coroutine> coroutineList = new LinkedList<Coroutine>();

        public void Start(IEnumerator ie)
        {
            coroutineList.AddLast(new Coroutine(ie));
        }

        public void Stop(IEnumerator ie)
        {
            try
            {
                //foreach (var item in coroutineList)
                //{

                //}
                //var c = coroutineList.Find(x => { return x.});
                //coroutineList.Remove(c);
            }
            catch (Exception e) { Log.Info(e.ToString()); }
        }

        public void UpdateCoroutine()
        {
            var node = coroutineList.First;
            while (node != null)
            {
                var cor = node.Value;

                bool ret = true;
                if (cor != null)
                    ret = cor.MoveNext();
                else
                    ret = false;

                if (!ret)
                    coroutineList.Remove(node);

                node = node.Next;
            }
        }

        public void Poll()
        {

        }
    }
}