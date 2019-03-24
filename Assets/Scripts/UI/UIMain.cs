using Lemon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.SceneManagement;

[UIDefine(pkgName = "Main", compName = "Main")]
public class UIMain : UIBase
{
    private GButton _enterGameBtn;

    public override void OnOpen()
    {
        base.OnOpen();
        _enterGameBtn = _root.GetChild("enterGameBtn").asButton;
        _enterGameBtn?.onClick.Add(OnEnterGameBtnClick);
    }

    private void OnEnterGameBtnClick()
    {
        Log.Info("[UIMain] Enter game!");
        CoroutineManager.Instance.Start(LoadBattleScene());
    }

    private IEnumerator LoadBattleScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SceneNormal3v3", LoadSceneMode.Single);

        asyncLoad.completed += OnLoadFinished;         
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

    private void OnLoadFinished(AsyncOperation obj)
    {
        UIManager.Instance().OpenUI("Battle", "BattleUI");
        Close();
    }
}
