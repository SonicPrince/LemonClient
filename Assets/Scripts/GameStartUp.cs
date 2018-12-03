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
        LMessage.AddListener(LoadEvent.LoadFinish, OnLoadFinished);
        CoroutineManager.Instance.Start(LoadLoaddingUI());
    }

    private void OnLoadFinished()
    {

    }

    private IEnumerator LoadLoaddingUI()
    {
        var request = LoadManager.Instance().LoadAsset<AssetBundle>("UI/loading");
        yield return request;

        if (request.Error == null)
        {
            UIPackage.AddPackage(request.Result);
            var ui = UIPackage.CreateObject("Loading", "LoadingUI").asCom;

            //以下几种方式都可以将view显示出来：
            //1，直接加到GRoot显示出来
            GRoot.inst.AddChild(ui);

            LoadTemplate.Instance().StartLoad();
            LoadAssetbundle.Instance().StartLoad(OnProgress);
        }
    }

    private void OnProgress(int arg1, int arg2)
    {
        Log.Info($"Load progress <{arg1} / {arg2}>");
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();
    }

}
