using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoneStage
{
    Enter,
    Exit
}

public class ZonePhase : MonoBehaviour
{
    [SerializeField]
    private ZoneStage _zoneStage;

    [SerializeField]
    private Collider _triggerCollider;

    public event DetectedCharacter OnDetectedCharacter;
    public delegate void DetectedCharacter(Character character, ZoneStage zoneStage);


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null)
            OnDetectCharacter(character);
    }


    private void OnDetectCharacter(Character character)
    {
        _triggerCollider.enabled = false;

        OnDetectedCharacter?.Invoke(character, _zoneStage);
    }
}