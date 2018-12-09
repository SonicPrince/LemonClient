using Lemon;
using System.Collections.Generic;

public interface LookupData<K>
{
    K GetKey();
}

// 单个字段作为唯一键的表格
public abstract class LookupDataTable<T, K> : DataTable<T> where T : LookupData<K>
{
    public virtual T Get(K key)
    {
        var ret = _lookup.GetValue(key);
        if (ret == null)
        {
            Log.Error($"[DataTable] {GetType().Name}.Get({key}) not found");
        }

        return ret;
    }

    public override void Clear()
    {
        _lookup.Clear();

        base.Clear();
    }

    protected override void OnLoaded()
    {
        _lookup = DictionaryUtils.Create(_data, val => val.GetKey());
    }

    protected Dictionary<K, T> _lookup = new Dictionary<K, T>();
}