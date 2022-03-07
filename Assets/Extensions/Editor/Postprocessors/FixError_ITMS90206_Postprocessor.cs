#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

using Xcode = UnityEditor.iOS.Xcode;

public class FixError_ITMS90206_Postprocessor
{
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string pathToBuiltProject)
	{
		if (buildTarget == BuildTarget.iOS)
		{
			var projPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";

			var project = new Xcode.PBXProject();
			project.ReadFromString(File.ReadAllText(projPath));

			var mainTargetGuid = project.GetUnityMainTargetGuid();
			project.SetBuildProperty(mainTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

			var unityFrameWorkTargetGuid = project.GetUnityFrameworkTargetGuid();
			project.SetBuildProperty(unityFrameWorkTargetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
			project.AddBuildProperty(unityFrameWorkTargetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/../../Frameworks");

			File.WriteAllText(projPath, project.WriteToString());
		}
	}
}
#endif