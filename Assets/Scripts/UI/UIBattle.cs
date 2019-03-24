using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.SceneManagement;

[UIDefine(pkgName = "Battle", compName = "BattleUI")]
public class UIBattle : UIBase
{

    public override void OnOpen()
    {
        base.OnOpen();
        Log.Info("[UIBattle]  ENTER BATTLEUI");
    }

}
