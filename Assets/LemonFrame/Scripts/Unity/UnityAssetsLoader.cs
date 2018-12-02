using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lemon
{
    internal class UnityAssetRequest<T> : CustomYieldInstruction, LoadRequest, ILoadRequest<T> where T : class
    {
        public UnityAssetRequest(string fileName) { this.fileName = fileName; }

        public Type assetType { get { return typeof(T); } }
        public string fileName { get; }

        public T Result { get; protected set; }
        public bool Completed { get; protected set; }
        public string Error { get; protected set; }

        public void SetResult(object obj) { Result = obj as T; Completed = true; }
        public void SetError(string error) { Error = error; Completed = true; }

        public override bool keepWaiting { get { return !Completed; } }
    }

    public class UnityAssetsLoader
    {
        private static UnityAssetsLoader instance;
        public static UnityAssetsLoader Instance()
        {
            if (instance == null)
            {
                instance = new UnityAssetsLoader();
                instance.Init();
            }
            return instance;
        }

        AssetBundleLoader abl = new AssetBundleLoader();

        string _rootpath = string.Empty;
        public void Init()
        {
            //LoadM
            _rootpath = GamePath.editorDataPath;
            LoadManager.Instance().AddLoader<AssetBundle>(request => { CoroutineManager.Instance.Start(LoadAssetbundle(request)); });
            LoadManager.Instance().AddLoader<UnityEngine.Object>(request => { CoroutineManager.Instance.Start(LoadObjectImpl(request)); });
        }

        private IEnumerator LoadAssetbundle(LoadRequest request)
        {
            var req1 = abl.LoadAssetBundle(_rootpath + request.fileName);
            yield return req1;
            if (req1.Error != null)
            {
                request.SetError(req1.Error);
                yield break;
            }

            request.SetResult(req1.Result);
        }

        private IEnumerator LoadObjectImpl(LoadRequest request)
        {
            string assetName;
            string fileName = SplitFileName(request.fileName, out assetName);

            var req1 = abl.LoadAssetBundle(_rootpath + fileName);
            yield return req1;

            if (req1.Error != null)
            {
                request.SetError(req1.Error);
                yield break;
            }

            var assetBundle = req1.Result;
            if (assetBundle.isStreamedSceneAssetBundle)
            {
                request.SetError("Cannot loadAsset from StreamedSceneAssetBundle");
                yield break;
            }

            var req2 = (string.IsNullOrEmpty(assetName))
                    ? assetBundle.LoadAllAssetsAsync(request.assetType)
                    : assetBundle.LoadAssetAsync(assetName, request.assetType);
            yield return req2;

            var asset = req2.asset;
            if (asset == null)
                request.SetError("AssetBundle.LoadAssetAsync failed");
            else
                request.SetResult(asset);
        }

        private static string SplitFileName(string fileName, out string assetName)
        {
            assetName = null;

            // try "module/assetBundle#assetName"
            int sep = fileName.LastIndexOf('#');
            if (sep < 0)
            {
                // not found, try "module/assetBundle/assetName"
                sep = fileName.IndexOf('/');

                if (sep >= 0)
                    sep = fileName.IndexOf('/', sep + 1);
            }

            if (sep >= 0)
            {
                assetName = fileName.Substring(sep + 1);
                fileName = fileName.Substring(0, sep);
            }

            return fileName;
        }


    }
}

