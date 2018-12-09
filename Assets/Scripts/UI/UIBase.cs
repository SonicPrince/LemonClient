using FairyGUI;
using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase
{
    protected GComponent _root;
    private string _packageName;
    private string _componentName;
    private bool isOpen = false;

    public void Show(string packageName, string componentName)
    {
        _packageName = packageName;
        _componentName = componentName;
        isOpen = true;

        if (_root == null)
        {
            DoLoad();
        }
        else
        {
            _root.visible = true;
        }
    }

    public void Close()
    {
        isOpen = false;

        if (_root != null)
            _root.visible = false;
    }

    private void DoLoad()
    {
        CoroutineManager.Instance.Start(LoadUI());
    }

    private IEnumerator LoadUI()
    {
        var request = UIPackageManager.Instance().GetPackage(_packageName);
        yield return request;

        var ui = UIPackage.CreateObject(_packageName, _componentName).asCom;

        if (ui != null)
        {
            GRoot.inst.AddChild(ui);
            _root = ui;

            if (isOpen)
                _root.visible = true;
            else
                _root.visible = false;
        }
    }

}