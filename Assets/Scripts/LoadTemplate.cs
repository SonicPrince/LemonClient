using Lemon;
using System.Collections;
using System.IO;
using UnityEngine;

public class LoadTemplate : LSingleton<LoadTemplate>
{
    private const string templatePath = "Templates";

    public void StartLoad()
    {
        DirectoryInfo root = new DirectoryInfo(GamePath.editorDataPath + "/" + templatePath);
        FileInfo[] files = root.GetFiles();

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
