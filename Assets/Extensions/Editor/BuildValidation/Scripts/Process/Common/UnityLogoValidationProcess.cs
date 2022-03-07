using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class UnityLogoValidationProcess : DefaultValidationProcess
    {
        public override void Execute(BaseBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Unity Logo");
            bool showUnityLogoEnabled = PlayerSettings.SplashScreen.showUnityLogo;
            if (showUnityLogoEnabled && !validationConfig.showUnityLogo)
            {
                OnValidationProcessFailed("Disable Unity Logo");
            }
        }
    }
}