using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceOptimization : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectToDeactivate;


    private void Start()
    {
        TurnOffMesh();
    }


    public void TurnOnMesh()
    {
        for (int i = 0; i < _objectToDeactivate.Length; i++)
        {
            var obj = _objectToDeactivate[i];

            obj.SetActive(true);
        }
    }


    public void TurnOffMesh()
    {
        for (int i = 0; i < _objectToDeactivate.Length; i++)
        {
            var obj = _objectToDeactivate[i];

            obj.SetActive(false);

        }
    }
}
