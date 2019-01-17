using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[UIDefine(pkgName = "Loading", compName = "LoadingUI")]
public class UILoading : UIBase
{
    public override void OnOpen()
    {
        base.OnOpen();

        Log.Info("OpenUILoading .. ");
    }

    public override void OnClose()
    {
        base.OnClose();
    }

}
