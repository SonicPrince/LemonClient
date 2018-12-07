using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class LoadTemplate : LSingleton<LoadTemplate>
{
    private readonly List<IDataTable> _tables = new List<IDataTable>();
    private const string templatePath = "Templates";

    public void InitTable()
    {
        Type baseType = typeof(IDataTable);

        AppDomain domain = AppDomain.CurrentDomain;
        var asms = domain.GetAssemblies();

        _tables.Clear();

        foreach (var asm in asms)
        {
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass || type.IsAbstract || type.IsNotPublic || type.IsNested || type.IsGenericType)
                    continue;

                if (!baseType.IsAssignableFrom(type))
                    continue;

                IDataTable table = null;

                // 获取单例
                MethodInfo method = type.GetMethod("Instance", BindingFlags.Static | BindingFlags.Public);
                if (method != null)
                    table = method.Invoke(null, null) as IDataTable;

                // 尝试直接创建一个实例
                if (table == null)
                    table = Activator.CreateInstance(type) as IDataTable;

                if (table == null)
                    return;

                // 添加到表格列表
                _tables.Add(table);
            }
        }
    }

    public void StartLoad()
    {
        var tableBase = typeof(IDataTable);

        Assembly assembly = Assembly.GetAssembly(tableBase);
        foreach (var item in assembly.GetTypes())
        {
            if (item.IsClass)
            {
                if (item.GetInterface(tableBase.Name) != null)
                {
                    Log.Info(item.Name);
                }
            }
        }


        //DirectoryInfo root = new DirectoryInfo(GamePath.editorDataPath + "/" + templatePath);
        //FileInfo[] files = root.GetFiles();

        //if (files != null && files.Length > 0)
        //{
        //    foreach (var file in files)
        //    {
        //        CoroutineManager.Instance.Start(LoadAllJson(file.Name));
        //    }
        //}
    }

    private IEnumerator LoadAllJson(string fileName)
    {
        var request = LoadManager.Instance().LoadText(templatePath + "/" + fileName);
        yield return request;

        if (request != null)
        {
            if (request.Result != null)
                Log.Info(request.Result);
            if (request.Error != null)
                Log.Info(request.Error);
        }
    }

}
