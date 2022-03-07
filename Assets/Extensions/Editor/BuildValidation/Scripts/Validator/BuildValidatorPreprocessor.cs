using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace TFPlay.BuildValidation
{
    public class BuildValidatorPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android && EditorUserBuildSettings.buildAppBundle)
            {
                ValidateAndroidBuild();
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS && !Application.isBatchMode && IsReleaseBuild())
            {
                ValidateiOSBuild();
            }
        }

        private void ValidateAndroidBuild()
        {
            AndroidBuildValidationConfig validationConfig = AndroidBuildValidationConfig.Default;
            AndroidBuildValidator buildValidator = new AndroidBuildValidator(validationConfig);
            buildValidator.Register(new UnityLogoValidationProcess());
            buildValidator.Register(new AutoGraphicsAPIValidationProcess());
            buildValidator.Register(new MinimumTargetAPILevelValidationProcess());
            buildValidator.Register(new TargetAPILevelValidationProcess());
            buildValidator.Register(new ScriptingBeckendValidationProcess());
            buildValidator.Register(new TargetArchitecturesValidationProcess());
            buildValidator.Register(new AndroidManifestDebuggableValidationProcess());
            buildValidator.Register(new KeystoreValidationProcess());
            buildValidator.ValidationFailed += BuildValidator_ValidationFailed;
            buildValidator.Validate();
        }

        private void ValidateiOSBuild()
        {
            iOSBuildValidationConfig validationConfig = iOSBuildValidationConfig.Default;
            iOSBuildValidator buildValidator = new iOSBuildValidator(validationConfig);
            buildValidator.Register(new UnityLogoValidationProcess());
            buildValidator.ValidationFailed += BuildValidator_ValidationFailed;
            buildValidator.Validate();
        }

        private void BuildValidator_ValidationFailed(string message)
        {
            throw new BuildFailedException(message);
        }

        private bool IsReleaseBuild()
        {
            return EditorUtility.DisplayDialog("Build Type", "Choose Build Type...", "Release", "Regular");
        }
    }
}