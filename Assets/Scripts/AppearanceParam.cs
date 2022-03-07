using UnityEngine;

[System.Serializable]
public struct AppearanceParam
{
    [SerializeField]
    private float _weightToChange;

    [SerializeField]
    private Color _barColor;

    [SerializeField]
    private string _name;

    [SerializeField]
    private GameObject _model;

    [SerializeField]
    private GameObject _lvlUpParticle;

    [SerializeField]
    private GameObject _lvlDownParticle;


    public float WeightToChange { get => _weightToChange; }

    public Color BarColor { get => _barColor; }

    public string Name { get => _name; }
    
    public GameObject Model { get => _model; }

    public GameObject LvlUpParticle { get => _lvlUpParticle; }

    public GameObject LvlDownParticle { get => _lvlDownParticle; }
}