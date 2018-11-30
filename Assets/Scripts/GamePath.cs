using System.IO;
using UnityEngine;

public static class GamePath
{
    public static string AssetDataPath;     // for File access
    public static string AssetDataURL;      // for WWW/WebRequest access

    public static string PersistentPath;    // for File access
    public static string PersistentURL;     // for WWW/WebRequest access

    public static string editorDataPath;

    static GamePath()
    {
        AssetDataPath = Path.GetFullPath("./");
        PersistentPath = Path.GetFullPath("./");

        AssetDataURL = Path2URL(AssetDataPath);
        PersistentURL = Path2URL(PersistentPath);
    }

    public static string Path2URL(string path)
    {
        if (path.StartsWith("jar"))
            return path;

        if (path.StartsWith("/"))
            return "file://" + path;
        else
            return "file:///" + path;
    }

    public static void SetEditorPath(string path)
    {
        editorDataPath = Application.dataPath + path;
    }
}
