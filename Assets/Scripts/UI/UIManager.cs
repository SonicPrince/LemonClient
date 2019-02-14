using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lemon;
using System;
using System.Reflection;

public class UIDefine : Attribute
{
    public string pkgName;
    public string compName;
}

public class UIManager : LSingleton<UIManager>
{
    Dictionary<string, Type> _dicUIDefine = new Dictionary<string, Type>();

    public override void Init()
    {
        base.Init();

        Type baseType = typeof(UIBase);

        AppDomain domain = AppDomain.CurrentDomain;
        var asms = domain.GetAssemblies();

        foreach (var asm in asms)
        {
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsClass || type.IsAbstract || type.IsNotPublic || type.IsNested || type.IsGenericType)
                    continue;

                if (!baseType.IsAssignableFrom(type))
                    continue;

                var define = (UIDefine)Attribute.GetCustomAttribute(type, typeof(UIDefine));
                if (define == null)
                    continue;

                string key = define.pkgName + "/" + define.compName;
                _dicUIDefine[key] = type;
            }
        }
    }

    public Dictionary<string, UIBase> _openUI = new Dictionary<string, UIBase>();
    public void OpenUI(string pkgName, string compName)
    {
        var key = pkgName + "/" + compName;

        var uibase = _openUI.GetValue(key);
        if (uibase != null)
        {
            uibase.Show(pkgName, compName);
            return;
        }

        if (!_dicUIDefine.ContainsKey(key))
            return;

        var type = _dicUIDefine[key];
        uibase = Activator.CreateInstance(type) as UIBase;
        if (uibase != null)
        {
            uibase.Show(pkgName, compName);
            _openUI[key] = uibase;
        }
    }

    public void Update()
    {
        foreach (var ui in _openUI)
        {
            ui.Value.Update();
        }
    }
}
