using Lemon;
using UnityEngine;

public class GameStartUp : MonoBehaviour
{
    public string dataPath = "../../../Lemon_assetsdata/";

    void Awake()
    {
        //InitLoadManager
        GamePath.SetEditorPath(dataPath);
#if UNITY_EDITOR
        LoadManager.Instance().SetRootPath(GamePath.editorDataPath);
#endif
    }

    private void Start()
    {
        LoadTemplate.Instance().StartLoad();
    }

    private void Update()
    {
        CoroutineManager.Instance.UpdateCoroutine();


    }








}
