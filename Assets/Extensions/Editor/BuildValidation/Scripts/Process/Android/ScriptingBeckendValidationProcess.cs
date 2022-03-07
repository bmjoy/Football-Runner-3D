using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class ScriptingBeckendValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Scripting Beckend");
            ScriptingImplementation currentScriptingBackend = PlayerSettings.GetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (currentScriptingBackend != validationConfig.scriptingBackend)
            {
                OnValidationProcessFailed("Set Scripting Beckend to " + validationConfig.scriptingBackend.ToString());
            }
        }
    }
}