using UnityEngine;
using System.IO;
using UnityEditor;

public abstract class ResourceSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;
    private const string AssetPath = "Assets/Resources";

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = AssetDatabase.LoadAssetAtPath<T>(GetAssetPath());
                if (instance == null)
                {
                    CreateAsset();
                }
                var inst = instance as ResourceSingleton<T>;
                if (inst != null)
                {
                    inst.OnInstanceLoaded();
                }
            }
            return instance;
        }
    }

    public void Make() { }
    static void CreateAsset()
    {
        instance = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(instance, GetAssetPath());
        EditorUtility.SetDirty(instance);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = instance;
    }

    public void Save()
    {
        var asset = AssetDatabase.LoadAssetAtPath<T>(GetAssetPath());
        if (asset != null)
        {
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }
    }

    protected virtual void OnInstanceLoaded()
    {
    }

    private static string GetAssetPath()
    {
        string path = Path.Combine(AssetPath, typeof(T).Name + ".asset");
        return path;
    }
}