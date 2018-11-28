using System;
using System.Collections.Generic;
using System.IO;

namespace Lemon
{
    public interface IDataTable
    {
        string GetName();

        int Count { get; }
        object GetElementAt(int index);
        Type GetElememtType();

        void Clear();

        void Parse(string text);
        void Serialize(Stream stream);
        void Deserialize(Stream stream);
    }

    // 数据表基类
    public class DataTable<T> : IDataTable
    {
        protected List<T> _data = new List<T>();

        protected DataTable()
        {
        }

        public virtual Type GetElememtType()
        {
            return typeof(T);
        }

        public virtual object GetElementAt(int index)
        {
            return _data[index];
        }

        public virtual string GetName()
        {
            return typeof(T).Name;
        }

        public virtual void Clear()
        {
            _data.Clear();
        }

        public virtual int Count
        {
            get { return _data.Count; }
        }

        public virtual List<T> Table
        {
            get { return _data; }
        }

        public virtual void Parse(string text)
        {
            DoLoaded();
        }

        public virtual void Serialize(Stream stream)
        {

        }

        public virtual void Deserialize(Stream stream)
        {

            DoLoaded();
        }

        protected void DoLoaded()
        {
            try
            {
                OnLoaded();
            }
            catch (Exception e)
            {
                throw new Exception("[DataTable] " + typeof(T).Name + " OnLoad() exception: " + e.Message);
            }

        }

        protected virtual void OnLoaded()
        {
        }

    }
}

