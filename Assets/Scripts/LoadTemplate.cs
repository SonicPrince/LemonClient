using Lemon;
using System.Collections;
using System.IO;
using UnityEngine;

public class LoadTemplate : LSingleton<LoadTemplate>
{
    private const string templatePath = "Templates";
    FileInfo[] files;
    int idx = 0;

    public void StartLoad()
    {
        //Log.Info("Application.dataPath:" + Application.dataPath);
        //Log.Info("Application.persistentDataPath:" + Application.persistentDataPath);
        //Log.Info("Application.streamingAssetsPath:" + Application.streamingAssetsPath);
        //Log.Info("Application.temporaryCachePath:" + Application.temporaryCachePath);

        DirectoryInfo root = new DirectoryInfo(GamePath.editorDataPath + "/" + templatePath);
        files = root.GetFiles();

        if (files != null && files.Length > 0)
        {
            foreach (var file in files)
            {
                CoroutineManager.Instance.Start(LoadAllJson(file.Name));
            }
        }
    }


    private IEnumerator LoadAllJson(string fileName)
    {
        var request = LoadManager.Instance().LoadText(templatePath + "/" + fileName);
        yield return request;

        if (request != null)
        {
            if (request.Result != null)
                Log.Info(request.Result);
            if (request.Error != null)
                Log.Info(request.Error);
        }
    }

}
