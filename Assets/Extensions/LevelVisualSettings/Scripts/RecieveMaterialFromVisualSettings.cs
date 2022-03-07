using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveMaterialFromVisualSettings : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        VisualSettingChanger.Instance.OnSettingsApply += SetMaterial;
    }

    private void OnDestroy()
    {
        VisualSettingChanger.Instance.OnSettingsApply -= SetMaterial;
    }

    private void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}