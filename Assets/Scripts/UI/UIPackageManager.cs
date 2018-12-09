using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lemon;
using FairyGUI;

class UIPackageRequest : CustomYieldInstruction, AsyncRequest<UIPackage>
{
    public string name;

    public bool Completed { get; set; }
    public UIPackage Result { get; set; }

    public override bool keepWaiting { get { return !Completed; } }
}

public class UIPackageManager : LSingleton<UIPackageManager>
{
    Dictionary<string, UIPackage> _dicCach = new Dictionary<string, UIPackage>();

    public override void Init()
    {
        base.Init();
        _dicCach.Clear();
    }

    public AsyncRequest<UIPackage> GetPackage(string name)
    {
        var request = new UIPackageRequest { name = name };

        UIPackage package;
        _dicCach.TryGetValue(name, out package);
        if (package != null)
        {
            request.Result = package;
            request.Completed = true;
        }
        else
        {
            CoroutineManager.Instance.Start(DoLoad(request));
        }

        return request;
    }

    private IEnumerator DoLoad(UIPackageRequest request)
    {
        if (request == null)
            yield break;

        // 再次检查Cache
        UIPackage package;
        _dicCach.TryGetValue(request.name, out package);

        if (package != null)
        {
            request.Result = package;
            request.Completed = true;
            yield break;
        }

        string requestLowercase = request.name.ToLowerInvariant();

        var requestAb = LoadManager.Instance().LoadAsset<AssetBundle>($"UI/{requestLowercase}");
        yield return requestAb;

        if (requestAb.Result == null)
        {
            request.Completed = true;
            Log.Warning($"[UIPackageManager] Could'n Load package {requestLowercase}");
            yield break;
        }

        package = UIPackage.AddPackage(requestAb.Result);
        if (package == null)
        {
            request.Completed = true;
            yield break;
        }
        package.LoadAllAssets();
        _dicCach[request.name] = package;

        request.Result = package;
        request.Completed = true;
        yield break;
    }
}