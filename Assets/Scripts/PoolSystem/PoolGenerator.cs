using System;
using System.Collections.Generic;
using UnityEngine;
using TFPlay;

public class PoolGenerator : MonoBehaviour
{
    [Serializable]
    public class PoolData
    {
        public string name;
        public PoolObject prefab;
        public int count;
    }

    [SerializeField]
    private List<PoolData> pools;

    private void OnValidate()
    {
        if (pools != null)
        {
            foreach (var p in pools)
            {
                p.name = p.prefab != null ? p.prefab.name : string.Empty;
            }
        }
    }

    private void Start()
    {
        CreatePools();
    }

    private void CreatePools()
    {
        foreach (var p in pools)
        {
            PoolManager.Instance.CreatePool(p.prefab.gameObject, p.count);
        }
    }
}
