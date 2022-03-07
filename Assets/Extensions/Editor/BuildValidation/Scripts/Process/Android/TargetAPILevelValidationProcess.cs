using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class TargetAPILevelValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Target API Level");
            AndroidSdkVersions currentTargetSdkVersion = PlayerSettings.Android.targetSdkVersion;
            if (currentTargetSdkVersion != validationConfig.requiredTargetSdkVersion)
            {
                OnValidationProcessFailed("Set Target API Level to " + validationConfig.requiredTargetSdkVersion);
            }
        }
    }
}
