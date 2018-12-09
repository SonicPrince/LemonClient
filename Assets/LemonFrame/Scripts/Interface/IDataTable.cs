using System;
using System.Collections;
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

        public object Reflect { get; private set; }

        public virtual void Parse(string text)
        {
            int startPos = text.IndexOf('[');
            int endPos = text.LastIndexOf(']');
            if (startPos > 0 && endPos > 0)
            {
                text = text.Substring(startPos, endPos + 1 - startPos);
            }

            var json = LitJson.JsonMapper.ToObject(text);
            var fields = DataHelper.GetFields(typeof(T));

            _data.Clear();

            for (int i = 0; i < json.Count; i++)
            {
                var row = json[i];
                var d = (T)Activator.CreateInstance(GetElememtType());
                if (d == null)
                {
                    Log.Error($"[IDataTable] It's something error to create Instance{GetElememtType()}");
                    return;
                }

                var e = ((IDictionary)row).GetEnumerator();
                while (e.MoveNext())
                {
                    var key = e.Key as string;
                    var val = e.Value as LitJson.JsonData;

                    if (fields.ContainsKey(key))
                    {
                        var field = fields[key];
                        if (field != null)
                            DataHelper.SetFieldValue(d, field, val);
                    }
                }

                _data.Add(d);
            }

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

