using UnityEngine;

namespace TFPlay.BuildValidation
{
    public class AndroidBuildValidator : BaseBuildValidator<AndroidBuildValidationConfig>
    {
        private const string AABCheckListURL = "https://www.notion.so/24play/663d46a8597a4f82a58ddbb4418df014";

        public AndroidBuildValidator(AndroidBuildValidationConfig validationConfig) : base(validationConfig)
        {

        }

        protected override void OnValidationFailed()
        {
            Application.OpenURL(AABCheckListURL);
            base.OnValidationFailed();
        }
    }
}