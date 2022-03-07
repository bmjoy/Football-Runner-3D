using System;
using System.Collections.Generic;
using UnityEngine;
using RenderSettings = UnityEngine.RenderSettings;

public class VisualSettingChanger : MonoSingleton<VisualSettingChanger>
{
    [Header("References")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private MeshRenderer waterMeshRenderer;

    [Space] [Header("Settings")]
    [SerializeField] private bool randomSettings;
    [SerializeField] private List<LevelVisualSettings> levelsVisualSettings = new List<LevelVisualSettings>();

    public event Action<Material> OnSettingsApply;
    
    private void Start()
    {
        GameC.Instance.OnLevelLoaded += SetVisualSettings;
    }

    private void OnDestroy()
    {
        GameC.Instance.OnLevelLoaded -= SetVisualSettings;
    }

    private void SetVisualSettings(int sceneId)
    {
        var settings = GetSettings(SLS.Data.Game.Level.Value);
        if(!settings) return;

        if (settings.skyboxMaterial) RenderSettings.skybox = settings.skyboxMaterial;
        RenderSettings.subtractiveShadowColor = settings.shadowColor;

        RenderSettings.fog = settings.enableFog;
        if (settings.enableFog)
        {
            RenderSettings.fogColor = settings.fogColor;
            RenderSettings.fogDensity = settings.fogDensity;
            RenderSettings.fogStartDistance = settings.fogStartDistance;
            RenderSettings.fogEndDistance = settings.fogEndDistance;
        }

        if (directionalLight)
        {
            directionalLight.color = settings.lightColor;
            directionalLight.intensity = settings.lightIntensity;
        }

        if (settings.waterMaterial && waterMeshRenderer)
        {
            waterMeshRenderer.material = settings.waterMaterial;
        }
        
        if(settings.objectsMaterial)
            OnSettingsApply?.Invoke(settings.objectsMaterial);
    }

    private LevelVisualSettings GetSettings(int sceneId)
    {
        if (levelsVisualSettings.Count == 0) return null;

        var index = randomSettings
            ? UnityEngine.Random.Range(0, levelsVisualSettings.Count)
            : (sceneId - 1) % levelsVisualSettings.Count;
        
        return levelsVisualSettings[index];
    }
}