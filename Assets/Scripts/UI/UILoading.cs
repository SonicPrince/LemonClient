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
    private GGraph _click;

    private float count = 0;
    private float max = 0;
    private bool beginLoad = false;

    public override void OnOpen()
    {
        base.OnOpen();

        var progreebar = _root.GetChild("progress");
        if (progreebar != null)
            _progress = progreebar as GProgressBar;

        var _logo = _root.GetChild("logo") as GImage;
        var _click = _root.GetChild("click") as GGraph;

        count = 0;
        max = 0;
        beginLoad = false;

        _click.onClick.Add(OnBtnClick);
        Log.Info("OpenUILoading .. ");
    }

    private void OnBtnClick(EventContext context)
    {
        //关闭Loading，打开Main界面
        UIManager.Instance().OpenUI("Main","LoginUI");
        //close
        Close();
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
    }
}
