using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public static class LevelsControllerToolbarButton
{
	private const string enabledText = "Toggle level testing\n\nLevel is in test";
	private const string disanabledText = "Toggle level testing\n\nLevel is NOT in test";

	static LevelsControllerToolbarButton()
	{
		ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
	}

	static void OnToolbarGUI()
	{
		var image = EditorGUIUtility.IconContent(@"UnityEditor.GameView@2x").image;
		var enabled = !GUILayout.Toggle(
			!SceneTester.Enabled,
			new GUIContent(null, image, SceneTester.Enabled ? enabledText : disanabledText),
			"Command"
		);

		SceneTester.SetEnabled(enabled);
	}
}
