using UnityEditor;
using System;

namespace TFPlay.BuildIncrementor
{
    [Serializable]
    public class AndroidBuildPlatformSettings : BuildPlatformSettings
    {
        public override void UpdateVersionNumber()
        {
            base.UpdateVersionNumber();
            PlayerSettings.Android.bundleVersionCode = MajorVersion * 10000 + MinorVersion * 1000 + BuildVersion;
        }
    }
}
