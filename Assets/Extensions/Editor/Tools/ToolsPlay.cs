using UnityEditor;
using UnityEngine;

public static class ToolsPlay
{
	[MenuItem("Tools/Play/Clear Level Data")]
	public static void ClearLevelData()
	{
		var saveLoad = new SaveLoadSystem();
		saveLoad.Data.Game.Level.Value = 1;
	}

	[MenuItem("Tools/Play/Clear Coins Data")]
	public static void ClearCoinslData()
	{
		var saveLoad = new SaveLoadSystem();
		saveLoad.Data.Game.Coins.Value = 0;
	}

	[MenuItem("Tools/Play/Save FB Setting")]
	public static void SaveFB()
	{
		var fbs = AssetDatabase.LoadAssetAtPath("Assets/FacebookSDK/SDK/Resources/FacebookSettings.asset", typeof(Object));

		if (fbs == null)
		{
			Debug.LogWarning("Can't find file Assets/FacebookSDK/SDK/Resources/FacebookSettings.asset");
			return;
		}

		EditorUtility.SetDirty(fbs);
		AssetDatabase.SaveAssets();
	}
}