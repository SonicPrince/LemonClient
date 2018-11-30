using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Lemon;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using Tables;

public class GameStartUp : MonoBehaviour
{

    void Start()
    {
        //InitLoadManager
        LoadManager.Instance().SetRootPath(@"E:\Unity2017Work\Lemon\Lemon_assetsdata\Templates");
        CoroutineManager.Instance.Start(LoadJson());
    }

    private IEnumerator LoadJson()
    {
        var request = LoadManager.Instance().LoadText("Avatar.json");

        yield return request;

        if (request != null)
        {
            if (request.Result != null)
                Log.Info(request.Result);
            if (request.Error != null)
                Log.Info(request.Error);
        }
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();


    }








}
