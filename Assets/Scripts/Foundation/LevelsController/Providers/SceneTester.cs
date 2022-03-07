#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester
{
    private const string TEST_SCENE_ENABLED_KEY = "SceneTesterEnabled";
    public static bool Enabled { get; private set; }

    [DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        Enabled = EditorPrefs.GetBool(TEST_SCENE_ENABLED_KEY, false);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoadRuntimeMethod()
    {
        if (!Enabled)
            return;

        if (SceneManager.GetSceneAt(0).buildIndex == 0)
        {
            Enabled &= SceneManager.sceneCount > 1;
            return;
        }

        var scene = SceneManager.GetSceneAt(0);
        var path = scene.path;
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(path, LoadSceneMode.Additive);
    }

    public static void SetEnabled(bool enabled)
    {
        Enabled = enabled;
        EditorPrefs.SetBool(TEST_SCENE_ENABLED_KEY, Enabled);
    }
}
#endif