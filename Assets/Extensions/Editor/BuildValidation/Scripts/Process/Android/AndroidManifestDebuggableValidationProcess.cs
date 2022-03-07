using System.Xml;

namespace TFPlay.BuildValidation
{
    public class AndroidManifestDebuggableValidationProcess : BaseAndroidValidationProcess
    {
        private const string AndroidManifestFilePath = "Assets/Plugins/Android/AndroidManifest.xml";
        private const string ApplicationNodeTagName = "application";
        private const string AndroidDebuggableAttributeName = "android:debuggable";

        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking AndroidManifest Debuggable");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(AndroidManifestFilePath);
            XmlNode applicationNode = xmlDocument.GetElementsByTagName(ApplicationNodeTagName).Item(0);
            XmlAttribute debuggableAttribute = applicationNode.Attributes[AndroidDebuggableAttributeName];
            if (debuggableAttribute == null)
            {
                OnValidationProcessFailed("Add android:debuggable=\"false\" inside AndroidManifest.xml");
            }
            else
            {
                bool debuggableEnabled = bool.Parse(debuggableAttribute.Value.ToLowerInvariant());
                if (debuggableEnabled)
                {
                    OnValidationProcessFailed("Set android:debuggable=\"false\" inside AndroidManifest.xml");
                }
            }
        }
    }
}
