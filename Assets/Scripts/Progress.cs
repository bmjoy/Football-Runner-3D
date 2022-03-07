using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    private const float MINIMUM_WEIGHT = 0f;

    [SerializeField]
    private AppearanceParam[] _appearance;

    [SerializeField]
    private ChangeAppearance _changeAppearance;

    [Header("Settings")]
    [SerializeField]
    private float _maxWeight = 100f;

    [SerializeField]
    private float _currentWeight;

    private int _previousLevel;

    private PickUpItem _pickUpItem;

    public event LvlChanged OnLvlChanged;
    public delegate void LvlChanged(bool isUpped);

    public event LowWeight OnLowWeight;
    public delegate void LowWeight();


    private void OnEnable()
    {
        _pickUpItem = GetComponentInChildren<PickUpItem>();
    }


    private void Start()
    {
        _pickUpItem.OnPickUp += OnItemPicked;

        StartAppearance();
    }


    private void StartAppearance()
    {
        if (_changeAppearance != null)
            _changeAppearance.SetStartParameters(_appearance[0]);
    }


    private void OnItemPicked(Item item)
    {
        var weight = item.Weight;

        ChangeProgress(weight);
    }


    public void SetWeight(float weight)
    {
        _currentWeight = weight;

        OnWeightChange();
    }


    public void ChangeProgress(float weight)
    {
        _currentWeight += weight;

        OnWeightChange();
    }


    private void OnWeightChange()
    {
        CorrectWeightIfLimits();

        CheckAppearance();
        UpdateProgressBar();
    }


    private void CheckAppearance()
    {
        for (int i = _appearance.Length - 1; i >= 0; i--)
        {
            if (_currentWeight >= _appearance[i].WeightToChange)
            {
                if (_previousLevel != i)
                    ChangeAppearance(_appearance[i]);

                CheckLvlChanges(i);

                _previousLevel = i;

                break;
            }
        }
    }


    private void CheckLvlChanges(int i)
    {
        if (_appearance[0].LvlDownParticle != null)
        {
            if (_previousLevel < i)
                OnLvlChanged?.Invoke(true);
            else if (_previousLevel > i)
                OnLvlChanged?.Invoke(false);
        }
    }


    private void UpdateProgressBar()
    {
        if (_changeAppearance != null)
            _changeAppearance.UpdateProgressBar(_currentWeight);
    }


    private void ChangeAppearance(AppearanceParam param)
    {
        if (_changeAppearance != null)
            _changeAppearance.SetAppearance(param);
    }


    private void CorrectWeightIfLimits()
    {
        if (_currentWeight > _maxWeight)
            _currentWeight = _maxWeight;

        if (_currentWeight <= MINIMUM_WEIGHT)
            OnMinimumWeight();
    }


    private void OnMinimumWeight()
    {
        OnLowWeight?.Invoke();
    }


    public float CurrentWeight { get => _currentWeight; }
}