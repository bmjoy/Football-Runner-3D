using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private SpawnParticle _spawnParticle;

    [SerializeField]
    private Collider _triggerCollider;

    [SerializeField, Header("Content")]
    private Image _iconSprite;

    [SerializeField]
    private Image _backgroundSprite;

    [SerializeField]
    private TextMeshProUGUI _answerText;

    [SerializeField]
    private Color _rightAnswerColor;

    [SerializeField]
    private Color _wrongAnswerColor;

    private float _weight;

    public event GateEnter OnGateEntered;
    public delegate void GateEnter(Gate gate);


    public void SetUp(GateParam param)
    {
        _weight = param.Weight;

        if (!param.IsWithText)
        {
            WithoutTextCase(param);
        }
        else
        {
            WithTextCase(param);
        }

        SetSpriteColor(param.Weight);
    }


    private void WithoutTextCase(GateParam param)
    {
        if (param.IconSprite == null)
        {
            _iconSprite.gameObject.SetActive(false);
            _answerText.text = param.Answer;
        }
        else
        {
            _answerText.gameObject.SetActive(false);
            _iconSprite.sprite = param.IconSprite;
        }
    }


    private void WithTextCase(GateParam param)
    {
        _iconSprite.sprite = param.IconSprite;
        _answerText.text = param.Answer;
    }


    private void SetSpriteColor(float weight)
    {
        var spriteColor = Color.white;

        if (weight > 0)
            spriteColor = _rightAnswerColor;
        else if (weight < 0)
            spriteColor = _wrongAnswerColor;

        _backgroundSprite.color = spriteColor;
    }


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null)
            OnDetect(character);
    }


    private void OnDetect(Character character)
    {
        character.ChangeWeight(_weight);

        OnGateEntered?.Invoke(this);
    }


    public void SpawnParticles(ParticleType type)
    {
        _spawnParticle.SpawnParticles(type);
    }


    public void DeactivateGate()
    {
        _iconSprite.gameObject.SetActive(false);
        _answerText.gameObject.SetActive(false);
        _backgroundSprite.gameObject.SetActive(false);
    }


    public void DeactivateCollider()
    {
        _triggerCollider.enabled = false;
    }


    public float Weight { get => _weight; }
}
