using UnityEngine;

namespace TFPlay.BuildValidation
{
    public abstract class BaseValidationProcess<T> : IValidationProcess<T> where T : BaseBuildValidationConfig
    {
        public abstract void Execute(T validationConfig);
        public event ErrorMessageHandler Failed;

        protected virtual void OnValidationProcessFailed(string errorText)
        {
            Failed.Invoke(errorText);
        }

        protected void ShowMessage(string message)
        {
            Debug.Log(message);
        }
    }
}

