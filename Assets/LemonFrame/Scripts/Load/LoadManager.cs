using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lemon
{
    public interface ILoadRequest : AsyncOp
    {
        string fileName { get; }

        string Error { get; }
    }

    public interface ILoadRequest<T> : AsyncRequest<T>, ILoadRequest where T : class
    {
    }

    public interface LoadRequest
    {
        Type assetType { get; }
        string fileName { get; }

        void SetResult(object obj);
        void SetError(string error);
    }

    public class LoadRequest<T> : LoadRequest, ILoadRequest<T> where T : class
    {
        public LoadRequest(string fileName) { this.fileName = fileName; }

        public Type assetType { get { return typeof(T); } }
        public string fileName { get; }

        public T Result { get; protected set; }
        public bool Completed { get; protected set; }
        public string Error { get; protected set; }

        public void SetResult(object obj) { Result = obj as T; Completed = true; }
        public void SetError(string error) { Error = error; Completed = true; }
    }

    public class LoadManager : LSingleton<LoadManager>
    {
        public virtual ILoadRequest<string> LoadText(string fileName)
        {
            return LoadAsset<string>(fileName);
        }

        public virtual ILoadRequest<byte[]> LoadBinary(string fileName)
        {
            return LoadAsset<byte[]>(fileName);
        }

        public virtual ILoadRequest<T> LoadAsset<T>(string fileName) where T : class
        {
            var request = new LoadRequest<T>(fileName);

            if (string.IsNullOrEmpty(_rootPath))
            {
                Log.Error("[LoadManager] You should Do LoadManager.SetRootPath() First!");
                return request;
            }

            foreach (var loader in _loaders)
            {
                if (loader.Invoke(request))
                    return request;
            }

            request.SetError($"[LoadManager] Has no loader to load the typeof: <{typeof(T)}>");
            return request;
        }


        List<Loader> _loaders = new List<Loader>();

        public void AddLoader<T>(Action<LoadRequest> loader)
        {
            if (loader == null)
                return;

            _loaders.Add(new Loader { type = typeof(T), loader = loader });
        }

        public override void Init()
        {
            base.Init();
            AddLoader<string>(request => CoroutineManager.Instance.Start(LoadAsyncImpl(request, File.ReadAllText)));
            AddLoader<byte[]>(request => CoroutineManager.Instance.Start(LoadAsyncImpl(request, File.ReadAllBytes)));
        }

        private string _rootPath;

        /// <summary>
        /// 初始化根目录
        /// 所有文件完整路径为：rootPath+"\\"+fileName
        /// </summary>
        /// <param name="rootPath">AllFilesPath is rootPath+"\\"+fileName</param>
        public void SetRootPath(string rootPath)
        {
            _rootPath = rootPath;
        }

        private IEnumerator LoadAsyncImpl(LoadRequest request, Func<string, object> syncLoadFunc)
        {
            var req = Task.Run(() =>
            {
                try
                {
                    return syncLoadFunc(_rootPath + "\\" + request.fileName);
                }
                catch (Exception e)
                {
                    return e;
                }
            });

            yield return req;

            if (req.Result is Exception)
                request.SetError((req.Result as Exception).Message);
            else
                request.SetResult(req.Result);
        }

        private class Loader
        {
            public Type type;
            public Action<LoadRequest> loader;

            public bool Invoke(LoadRequest request)
            {
                if (!type.IsAssignableFrom(request.assetType))
                    return false;

                loader(request);
                return true;
            }
        }
    }
}





