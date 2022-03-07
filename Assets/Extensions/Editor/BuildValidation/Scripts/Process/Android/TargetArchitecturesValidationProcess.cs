using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class TargetArchitecturesValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Target Architectures");
            AndroidArchitecture targetArchitectures = PlayerSettings.Android.targetArchitectures;
            if (validationConfig.targetArchitectures.HasFlag(AndroidArchitecture.ARMv7) && (targetArchitectures & AndroidArchitecture.ARMv7) != AndroidArchitecture.ARMv7)
            {
                OnValidationProcessFailed("Set ARMv7 Target Architectures");
            }
            if (validationConfig.targetArchitectures.HasFlag(AndroidArchitecture.ARM64) && (targetArchitectures & AndroidArchitecture.ARM64) != AndroidArchitecture.ARM64)
            {
                OnValidationProcessFailed("Set ARM64 Target Architectures");
            }
        }
    }
}
