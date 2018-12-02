using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lemon;
using System.IO;

public class LoadAssetbundle : LSingleton<LoadAssetbundle>
{
    private const string modelPath = "Model";
    private const string uiPath = "UI";

    public void StartLoad()
    {
        counter = 0;
        LoadModel(modelPath);
        LoadModel(uiPath);
    }

    private int counter = 0;
    private void LoadModel(string path)
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
                CoroutineManager.Instance.Start(LoadAb(path + "/" + file.Name));
            }
        }
    }

    private int finish = 0;
    private IEnumerator LoadAb(string fileName)
    {
        var request = LoadManager.Instance().LoadAsset<AssetBundle>(fileName);
        yield return request;
        finish++;
        if (finish >= counter)
        {
            LMessage.Broadcast(LoadEvent.LoadFinish);
        }
    }

}
