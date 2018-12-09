using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LitJson;
using Tables;
using UnityEngine;

public static class DataHelper
{

    public static int ToInt(this string text)
    {
        int result = 0;
        if (!string.IsNullOrEmpty(text))
            int.TryParse(text, out result);

        return result;
    }

    public static long ToLong(this string text)
    {
        long result = 0;
        if (!string.IsNullOrEmpty(text))
            long.TryParse(text, out result);

        return result;
    }

    public static float ToFloat(this string text)
    {
        float result = 0;
        if (!string.IsNullOrEmpty(text))
            float.TryParse(text, out result);

        return result;
    }

    public static Dictionary<string, FieldInfo> GetFields(Type type)
    {
        Dictionary<string, FieldInfo> ret = new Dictionary<string, FieldInfo>();
        var fields = type.GetFields();
        foreach (var item in fields)
        {
            ret[item.Name] = item;
        }

        return ret;
    }

    internal static void SetFieldValue<T>(T d, FieldInfo field, JsonData val)
    {
        object result = null;

        switch (val.GetJsonType())
        {
            case LitJson.JsonType.String:
                result = val.ToString();
                break;
            case LitJson.JsonType.Int:
                result = int.Parse(val.ToString());
                break;
            case LitJson.JsonType.Long:
                result = long.Parse(val.ToString());
                break;
            case LitJson.JsonType.Double:
                result = float.Parse(val.ToString());
                break;
        }

        field.SetValue(d, result);
    }
}
