using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZone : MonoBehaviour
{
    private Enemy[] _enemy;
    private ZonePhase[] _enemyZonePhase;


    private void Start()
    {
        GetEnemies();
        InitZonePhases();
    }


    private void GetEnemies()
    {
        _enemy = GetComponentsInChildren<Enemy>();
    }


    private void InitZonePhases()
    {
        _enemyZonePhase = GetComponentsInChildren<ZonePhase>();

        for (int i = 0; i < _enemyZonePhase.Length; i++)
            _enemyZonePhase[i].OnDetectedCharacter += OnDetect;
    }


    private void OnDetect(Character character, ZoneStage zoneStage)
    {
        switch (zoneStage)
        {
            case ZoneStage.Enter:
                OnEnter(character);

                break;
            case ZoneStage.Exit:
                OnExit();

                break;
        }
    }


    private void OnEnter(Character character)
    {
        character.GetComponent<Progress>().OnLowWeight += OnCharacterDie;

        for (int i = 0; i < _enemy.Length; i++)
        {
            _enemy[i].SetTarget(character);
            _enemy[i].StartFollow();
        }
    }


    private void OnExit()
    {
        for (int i = 0; i < _enemy.Length; i++)
        {
            _enemy[i].StopFollow();
        }
    }


    private void OnCharacterDie()
    {
        OnExit();
    }
}
