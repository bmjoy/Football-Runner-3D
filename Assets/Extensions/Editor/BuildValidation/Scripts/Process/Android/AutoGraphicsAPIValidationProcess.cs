using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class AutoGraphicsAPIValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Auto Graphics API");
            bool autoGraphicsAPIEnabled = PlayerSettings.GetUseDefaultGraphicsAPIs(BuildTarget.Android);
            if (!autoGraphicsAPIEnabled && validationConfig.useAutoGraphicsAPI)
            {
                OnValidationProcessFailed("Enable Auto Graphics API");
            }
        }
    }
}
