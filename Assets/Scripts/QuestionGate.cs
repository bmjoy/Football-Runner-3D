using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGate : MonoBehaviour
{
    private const float MOVE_SPEED_DIVIDER = 1.85f;

    [Header("Gate settings")]
    [SerializeField]
    private GateParam[] _gateParam;

    [SerializeField, Multiline]
    private string _question;

    private float _previousSpeed;

    private ZonePhase[] _zonePhase;

    private List<Gate> _gate;


    private void OnEnable()
    {
        InitGates();
        InitZonePhases();
    }


    private void Start()
    {
        SetUpGates();
    }


    private void InitGates()
    {
        _gate = new List<Gate>(GetComponentsInChildren<Gate>());

        for (int i = 0; i < _gate.Count; i++)
            _gate[i].OnGateEntered += OnGateEntered;
    }


    private void InitZonePhases()
    {
        _zonePhase = GetComponentsInChildren<ZonePhase>();

        for (int i = 0; i < _zonePhase.Length; i++)
            _zonePhase[i].OnDetectedCharacter += OnDetect;
    }


    private void OnDetect(Character character, ZoneStage zoneStage)
    {
        switch (zoneStage)
        {
            case ZoneStage.Enter:
                OnEnter(character);

                break;
            case ZoneStage.Exit:
                OnExit(character);

                break;
        }
    }


    private void SetUpGates()
    {
        for (int i = 0; i < _gate.Count; i++)
        {
            _gate[i].SetUp(_gateParam[i]);
        }
    }


    private void OnEnter(Character character)
    {
        _previousSpeed = character.MoveSpeed;

        character.SetMoveSpeed(character.MoveSpeed / MOVE_SPEED_DIVIDER);

        character.ChangeMovableBehaviour(new ForwardMovePattern(character.transform, character.MoveSpeed));
        character.ExecuteMovement();

        QuestionPanel.Instance.ActivatePanel(_question);
    }


    private void OnExit(Character character)
    {
        character.SetMoveSpeed(_previousSpeed);

        character.ChangeMovableBehaviour(new ForwardMovePattern(character.transform, character.MoveSpeed));
        character.ExecuteMovement();

        QuestionPanel.Instance.DeactivatePanel();
    }


    private void OnGateEntered(Gate gate)
    {
        if (gate.Weight >= 0)
            CorrectAnswer(gate);
        else
            IncorrectAnswer(gate);

        for (int i = 0; i < _gate.Count; i++)
            _gate[i].DeactivateCollider();

        gate.DeactivateGate();
    }


    private void CorrectAnswer(Gate gate)
    {
        gate.SpawnParticles(ParticleType.Good);
    }


    private void IncorrectAnswer(Gate gate)
    {
        gate.SpawnParticles(ParticleType.Bad);
    }
}
