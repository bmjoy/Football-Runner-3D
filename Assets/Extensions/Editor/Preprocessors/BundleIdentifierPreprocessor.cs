using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class BundleIdentifierPreprocessor : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            if (PlayerSettings.applicationIdentifier.ToLowerInvariant() == "com.game.name")
            {
                string path = Path.GetDirectoryName(Application.dataPath);
                string lastFolderName = Path.GetFileName(path);
                string newTempBundleIdentifier = "com.game." + lastFolderName.RemoveNumbers().ToLowerInvariant();
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, newTempBundleIdentifier);
            }    
        }
    }
}