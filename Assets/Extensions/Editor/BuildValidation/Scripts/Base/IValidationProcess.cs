namespace TFPlay.BuildValidation
{
    public delegate void ErrorMessageHandler(string message);

    public interface IValidationProcess<in T> where T : BaseBuildValidationConfig
    {
        void Execute(T validationConfig);
        event ErrorMessageHandler Failed;
    }
}
