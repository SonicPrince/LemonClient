using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lemon
{

    public class AssetBundleLoader
    {
        private Dictionary<string, AssetBundle> _dicAssetbundCach = new Dictionary<string, AssetBundle>();

        public ILoadRequest<AssetBundle> LoadAssetBundle(string fileName)
        {
            var request = new UnityAssetRequest<AssetBundle>(fileName);
            AssetBundle ret;
            _dicAssetbundCach.TryGetValue(fileName, out ret);
            if (ret != null)
            {
                request.SetResult(ret);
                return request;
            }

            LoadAssetBundle(request);

            return request;
        }

        private void LoadAssetBundle(UnityAssetRequest<AssetBundle> request)
        {
            CoroutineManager.Instance.Start(LoadAssetBundleImpl(request));
        }

        private IEnumerator LoadAssetBundleImpl(UnityAssetRequest<AssetBundle> request)
        {
            //Log.Info("LoadAssetBundleImpl " + c.fileName);

            var req1 = AssetBundle.LoadFromFileAsync(request.fileName);
            yield return req1;

            var assetBundle = req1.assetBundle;
            if (assetBundle != null)
            {
                _dicAssetbundCach[request.fileName] = req1.assetBundle;
                request.SetResult(req1.assetBundle);
            }
            else
            {
                request.SetError($"[AssetBundleLoader] could'n load assetbundle {request.fileName}");
            }
        }
    }

}

