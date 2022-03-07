using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class MinimumTargetAPILevelValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Minimum API Level");
            AndroidSdkVersions currentMinSdkVersion = PlayerSettings.Android.minSdkVersion;
            if (currentMinSdkVersion != validationConfig.requiredMinSdkVersion)
            {
                OnValidationProcessFailed("Set Minimum API Level to " + validationConfig.requiredMinSdkVersion);
            }
        }
    }
}
