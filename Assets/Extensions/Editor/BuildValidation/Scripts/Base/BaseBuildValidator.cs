using UnityEditor;
using System.Text;
using System.Collections.Generic;

namespace TFPlay.BuildValidation
{
    public abstract class BaseBuildValidator<T> where T : BaseBuildValidationConfig 
    {
        private List<IValidationProcess<T>> processes = new List<IValidationProcess<T>>();
        private StringBuilder errorMessage = new StringBuilder();
        private T validationConfig;

        public event ErrorMessageHandler ValidationFailed;

        public BaseBuildValidator(T validationConfig)
        {
            this.validationConfig = validationConfig;
        }

        public void Register(IValidationProcess<T> process)
        {
            processes.Add(process);
            process.Failed += Process_Failed;
        }

        public void Validate()
        {
            foreach (var process in processes)
            {
                process.Execute(validationConfig);
            }

            if (HasBuildErrors())
            {
                OnValidationFailed();
            }
        }

        public bool HasBuildErrors()
        {
            return errorMessage != null && errorMessage.Length > 0;
        }

        public string GetErrorMessage()
        {
            return errorMessage.ToString();
        }

        protected virtual void OnValidationFailed()
        {
            ValidationFailed(GetErrorMessage());
        }

        private void Process_Failed(string error)
        {
            LogBuildError(error);
        }

        private void LogBuildError(string message)
        {
            if (errorMessage.Length == 0)
            {
                errorMessage.AppendLine("Fix these issues:");
            }

            errorMessage.AppendLine(" - " + message);
            EditorUtility.DisplayDialog("Error!", message, "OK");
        }
    }
}
