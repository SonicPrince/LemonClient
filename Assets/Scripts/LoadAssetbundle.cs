using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lemon;
using System.IO;
using System;

public class LoadAssetbundle : LSingleton<LoadAssetbundle>
{
    private const string modelPath = "Model";
    private const string uiPath = "UI";
    private const string scenePath = "Scenes";

    public void StartLoad(Action<int, int> loadProgress)
    {
        counter = 0;
        LoadModel(modelPath, loadProgress);
        LoadModel(uiPath, loadProgress);
        LoadModel(scenePath, loadProgress);
    }

    private int counter = 0;
    private void LoadModel(string path, Action<int, int> loadProgress)
    {
        DirectoryInfo root = new DirectoryInfo(GamePath.editorDataPath + "/" + path);
        FileInfo[] files = root.GetFiles();

        if (files != null && files.Length > 0)
        {
            foreach (var file in files)
            {
                if (file.Extension == ".manifest")
                    continue;

                counter++;
                CoroutineManager.Instance.Start(LoadAb(path + "/" + file.Name.ToLower(), loadProgress));
            }
        }
    }

    private int finish = 0;
    private IEnumerator LoadAb(string fileName, Action<int, int> loadProgress)
    {
        var request = LoadManager.Instance().LoadAsset<AssetBundle>(fileName);
        yield return request;

        finish++;
        loadProgress?.Invoke(finish, counter);
    }

}
