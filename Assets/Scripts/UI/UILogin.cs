using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

[UIDefine(pkgName = "Main", compName = "LoginUI")]
public class UILogin : UIBase
{
    private GButton _loginBtn;

    public override void OnOpen()
    {
        base.OnOpen();
        _loginBtn = _root.GetChild("loginBtn").asButton;
        _loginBtn?.onClick.Add(OnLoginBtnClick);

    }

    private void OnLoginBtnClick()
    {
        UIManager.Instance().OpenUI("Main", "Main");
        Close();
    }
}
