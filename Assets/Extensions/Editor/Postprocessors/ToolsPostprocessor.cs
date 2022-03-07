using UnityEngine;
using UnityEditor.Build.Reporting;
using UnityEditor.Build;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor;

public class ToolsPostprocessor : IProcessSceneWithReport
{
    public int callbackOrder => 0;

    public void OnProcessScene(Scene scene, BuildReport report)
    {
        if (Application.isPlaying)
            return;

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            if (scene.name == "MainScene")
            {
                GameObject[] gameObjects = scene.GetRootGameObjects();
                GameObject toolsHolder = gameObjects.FirstOrDefault(x => x.name == "Tools");
                if (toolsHolder != null)
                {
                    SetToolsActive(GetTools(toolsHolder.transform), !EditorUserBuildSettings.buildAppBundle);
                }
            }
        }
    }

    private Transform[] GetTools(Transform toolsHolder)
    {
        Transform[] tools = new Transform[toolsHolder.transform.childCount];
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i] = toolsHolder.transform.GetChild(i);
        }
        return tools;
    }

    private void SetToolsActive(Transform[] tools, bool isActive)
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].gameObject.SetActive(isActive);
        }
    }
}
