using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelVisualSettingsData", menuName = "24Play/ScriptableObjects/LevelVisualSettingsData", order = 1)]
public class LevelVisualSettings : ScriptableObject
{
    [Header("Skybox")]
    public Material skyboxMaterial;
    public Color shadowColor = new Color(107, 122, 160);
    
    [Space]
    [Header("Light")]
    public Color lightColor = Color.white;
    public float lightIntensity = 1f;

    [Space] [Header("Fog")]
    public bool enableFog = false;
    public Color fogColor = Color.gray;
    public FogMode fogMode = FogMode.Linear;
    public float fogDensity = .01f;
    public float fogStartDistance = 0f;
    public float fogEndDistance = 200f;

    [Space] [Header("Other")] 
    public Material waterMaterial;
    public Material objectsMaterial;
}