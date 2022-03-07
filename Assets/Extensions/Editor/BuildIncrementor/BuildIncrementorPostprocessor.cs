using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Linq;
using System.IO;

namespace TFPlay.BuildIncrementor
{
    public class BuildIncrementorPostprocessor
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (BuildVersionSettings.Instance.AutoIncrement)
            {
                Debug.Log("Build v" + BuildVersionSettings.Instance.CurrentPlatform.CurrentVersion);
                IncreaseBuild();
            }
        }

        private static void IncreaseBuild()
        {
            BuildVersionSettings.Instance.CurrentPlatform.IncreaseBuild();
            BuildVersionSettings.Instance.Save();
        }

        [MenuItem("Build/Create Version File")]
        private static void Create()
        {
            BuildVersionSettings.Instance.Make();
        }

        [MenuItem("Build/Increase Major Version")]
        private static void IncreaseMajor()
        {
            BuildVersionSettings.Instance.CurrentPlatform.IncreaseMajor();
            BuildVersionSettings.Instance.Save();
        }

        [MenuItem("Build/Increase Minor Version")]
        private static void IncreaseMinor()
        {
            BuildVersionSettings.Instance.CurrentPlatform.IncreaseMinor();
            BuildVersionSettings.Instance.Save();
        }
    }
}