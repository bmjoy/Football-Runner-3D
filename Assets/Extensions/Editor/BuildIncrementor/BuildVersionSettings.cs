using UnityEngine;
using UnityEditor;

namespace TFPlay.BuildIncrementor
{
    [InitializeOnLoad]
    public class BuildVersionSettings : ResourceSingleton<BuildVersionSettings>
    {
        [SerializeField]
        private bool autoIncrement = true;
        [SerializeField]
        private AndroidBuildPlatformSettings Android;
        [SerializeField]
        private iOSBuildPlatformSettings IOS;

        public BuildPlatformSettings CurrentPlatform
        {
            get
            {
#if UNITY_ANDROID
                return Android;
#elif UNITY_IOS
                return IOS;
#else
                return null;
#endif
            }
        }

        public bool AutoIncrement => autoIncrement;

        private void OnValidate()
        {
            UpdatePlatformVersionNumber();
        }

        protected override void OnInstanceLoaded()
        {
            UpdatePlatformVersionNumber();
        }

        private void UpdatePlatformVersionNumber()
        {
            CurrentPlatform.UpdateVersionNumber();
            Save();
        }
    }
}
