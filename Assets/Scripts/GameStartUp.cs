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
    private ILoadRequest<string> jsonFile;

    void Start()
    {
        //InitLoadManager
        LoadManager.Instance().SetRootPath(@"E:\Unity2017Work\Lemon\Lemon_assetsdata\Templates");

        jsonFile = LoadManager.Instance().LoadText("Avatar.json");
    }
   
    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();

        if (jsonFile != null)
        {
            if (jsonFile.Result != null)
                Log.Info(jsonFile.Result);
            if (jsonFile.Error != null)
                Log.Info(jsonFile.Error);
        }
    }








}
