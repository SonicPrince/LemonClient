using Lemon;
using System.Collections;
using UnityEngine;

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
        CoroutineManager.Instance.Start(LoadRole("role01"));
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();
    }

    private IEnumerator LoadRole(string fileName)
    {
        Log.Info("begin LoadRole:");
        var request = LoadManager.Instance().LoadAsset<Object>(@"E:\Unity2017Work\Lemon\Lemon_assetsdata\Model\role01#" + fileName);
        yield return request;

        Log.Info("request:" + request);
        if (request != null)
        {
            if (request.Result != null)
            {
                Log.Info(request.Result.ToString());
                var ab = request.Result;
               
                GameObject.Instantiate(ab);

            }
            if (request.Error != null)
                Log.Info(request.Error);

            Log.Info("sdasds");
        }
    }







}
