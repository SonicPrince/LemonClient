using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

[UIDefine(pkgName = "Loading", compName = "LoadingUI")]
public class UILoading : UIBase
{
    private GProgressBar _progress;
    private GImage _logo;

    private float count = 0;
    private float max = 0;
    private bool beginLoad = false;

    public override void OnOpen()
    {
        base.OnOpen();

        var progreebar = _root.GetChild("progress");
        if (progreebar != null)
            _progress = progreebar as GProgressBar;

        var logo = _root.GetChild("logo") as GImage;
        if (logo != null)
            _logo = logo as GImage;

        count = 0;
        max = 0;
        beginLoad = false;

        Log.Info("OpenUILoading .. ");
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_logo != null)
        {
            _logo.rotation++;
            if (_logo.rotation >= 180)
                _logo.rotation = 0;
        }

        if (!beginLoad)
            return;

        if (_progress == null)
            return;

        _progress.max = max;
        _progress.value = count;

        Log.Info("[UILoading] update finished");
    }

    public override void RegisterEvent()
    {
        base.RegisterEvent();
        LMessage.AddListener<int, int>(LoadEvent.LoadProgressUpdate, OnLoadUpdate);
    }

    private void OnLoadUpdate(int cur, int all)
    {
        count = cur;
        max = all;
        beginLoad = true;

        Log.Info($"[UILoading] OnLoadUpdate {count}/{max}");
    }
}
