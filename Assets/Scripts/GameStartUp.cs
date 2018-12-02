using Lemon;
using System;
using System.Collections;
using UnityEngine;
using FairyGUI;

public class GameStartUp : MonoBehaviour
{
    public string dataPath = "../../../Lemon_assetsdata/";

    void Awake()
    {
        //InitLoadManager
        GamePath.SetEditorPath(dataPath);
        UnityAssetsLoader.Instance().Init();
#if UNITY_EDITOR
        LoadManager.Instance().SetRootPath(GamePath.editorDataPath);
#endif
    }

    private void Start()
    {
        //LoadTemplate.Instance().StartLoad();
        LoadAssetbundle.Instance().StartLoad();
        //CoroutineManager.Instance.Start(LoadRole("role01"));
        LMessage.AddListener(LoadEvent.LoadFinish, OnLoadFinished);

        //CoroutineManager.Instance.Start(LoadBattleUI());
    }

    private void OnLoadFinished()
    {
        CoroutineManager.Instance.Start(LoadBattleUI());

    }

    private IEnumerator LoadBattleUI()
    {
        var request = LoadManager.Instance().LoadAsset<AssetBundle>("UI/battle");
        yield return request;
        Log.Info("LoadBattleUI");
        if (request.Error == null)
        {
            UIPackage.AddPackage(request.Result);
            var ui = UIPackage.CreateObject("Battle", "BattleUI").asCom;
            Log.Info("LoadBattleUI -----");
            
            //以下几种方式都可以将view显示出来：
            //1，直接加到GRoot显示出来
            GRoot.inst.AddChild(ui);
        }
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();
    }

}
