using Lemon;
using System;
using System.Collections;
using UnityEngine;
using FairyGUI;
using Tables;

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
        LMessage.AddListener<int>(LoadEvent.LoadFinish, OnLoadFinished);
        CoroutineManager.Instance.Start(LoadLoaddingUI());
    }

    private void OnLoadFinished(int pro)
    {
        Log.Info("----" + pro);
    }

    private IEnumerator LoadLoaddingUI()
    {
        var request = LoadManager.Instance().LoadAsset<AssetBundle>("UI/loading");
        yield return request;

        if (request.Error == null)
        {
            UIPackage.AddPackage(request.Result);
            var ui = UIPackage.CreateObject("Loading", "LoadingUI").asCom;

            GRoot.inst.AddChild(ui);

            LoadTemplate.Instance().StartLoad();
            LoadAssetbundle.Instance().StartLoad(OnProgress);
        }

        LMessage.Broadcast(LoadEvent.LoadFinish, 100, MessagerMode.DONT_REQUARE_LISTENNER);
    }

    private void OnProgress(int arg1, int arg2)
    {
        Log.Info($"Load progress <{arg1} / {arg2}>");

        if (arg1 >= arg2)
        {
            var tpl = (UITemplate)UITemplateTable.Instance().Get(1);
            if (tpl != null)
                Log.Info($"{tpl.sName}-{tpl.sPkgName}-{tpl.sCompName}");
        }
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();
    }

}
