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
        CoroutineManager.Instance.Start(DelayLoad());

        UIManager.Instance().OpenUI("Loading", "LoadingUI");
        DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator DelayLoad()
    {
        yield return new Lemon.WaitForSeconds(1f);

        LoadTemplate.Instance().StartLoad();
        LoadAssetbundle.Instance().StartLoad(OnProgress);
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
    }

    private void OnProgress(int arg1, int arg2)
    {
        Log.Info($"Load progress <{arg1} / {arg2}>");

        if (arg1 >= arg2)
        {
            //加载完成
            LMessage.Broadcast(LoadEvent.LoadFinish);
        }

        LMessage.Broadcast(LoadEvent.LoadProgressUpdate, arg1, arg2);
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();
        UIManager.Instance().Update();
    }

}
