using System;
using UnityEngine;

public class LevelsController : MonoSingleton<LevelsController>
{
    [SerializeField]
    private int levelOffset;

    private ILevelProvider levelProvider;

    public int LevelsCount => levelProvider != null ? levelProvider.LevelsCount : 0;

    public event Action OnLevelLoadingStart;
    public event Action<int> OnLevelLoaded;

    private void OnValidate()
    {
        if (levelProvider == null)
            levelProvider = GetComponent<ILevelProvider>();

        levelOffset = Mathf.Clamp(levelOffset, 0, levelProvider.LevelsCount - 1);
    }

    protected override void Awake()
    {
        base.Awake();

        levelProvider = GetComponent<ILevelProvider>();

        if (levelProvider == null)
            Debug.LogWarning("Провайдер не выбран! Так это не работает", this);

        levelProvider.OnLevelLoaded += TriggerEvent;
    }

    private void Start()
    {
        GameC.Instance.OnLevelStartLoading += LoadLevel;
    }

    private void TriggerEvent(int sceneId)
    {
        OnLevelLoaded?.Invoke(sceneId);
    }

    private void LoadLevel(int levelNumber)
    {
        OnLevelLoadingStart?.Invoke();
        levelProvider?.LoadLevel(AdjustLevelNumber(levelNumber));
    }

    private int AdjustLevelNumber(int levelNumber)
    {
        int levelIndex = levelNumber - 1;
        if (levelNumber > levelProvider.LevelsCount && levelOffset > 0)
        {
            levelNumber = (levelIndex - levelOffset) % (levelProvider.LevelsCount - levelOffset) + levelOffset;
        }
        else
        {
            levelNumber = levelIndex % levelProvider.LevelsCount;
        }
        return levelNumber + 1;
    }
}