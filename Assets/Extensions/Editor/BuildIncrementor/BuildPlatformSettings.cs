using UnityEditor;
using System;

namespace TFPlay.BuildIncrementor
{
    [Serializable]
    public class BuildPlatformSettings
    {
        public int MajorVersion = 1;
        public int MinorVersion;
        public int BuildVersion;
        public string CurrentVersion = "1.0.0";

        public void IncreaseMajor()
        {
            MajorVersion++;
            MinorVersion = 0;
            BuildVersion = 0;
            UpdateVersionNumber();
        }

        public void IncreaseMinor()
        {
            MinorVersion++;
            BuildVersion = 0;
            UpdateVersionNumber();
        }

        public void IncreaseBuild()
        {
            BuildVersion++;
            UpdateVersionNumber();
        }

        public virtual void UpdateVersionNumber()
        {
            CurrentVersion = MajorVersion + "." + MinorVersion + "." + BuildVersion;
            PlayerSettings.bundleVersion = CurrentVersion;
        }
    }
}
