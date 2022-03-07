using System;
using UnityEngine;

public class GameC : MonoSingleton<GameC>
{
    public event Action OnInitCompleted;
    public event Action OnShowFadeUI;
    public event Action<int> OnLevelStartLoading;
    public event Action<int> OnLevelLoaded;
    public event Action<bool> OnLevelEnd;

    private void Start()
    {
        LevelsController.Instance.OnLevelLoaded += InvokeOnLevelLoaded;
        Application.targetFrameRate = 60;
        this.DoAfterNextFrameCoroutine(() =>
        {
            OnInitCompleted?.Invoke();
            AnalyticsHelper.Init();
            LoadLevel();
        });
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LevelEnd(true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelEnd(false);
        }
    }
#endif

    public void LoadLevel()
    {
        SLS.Save(); 
        OnShowFadeUI?.Invoke();
    }

    public void InvokeOnStartLevelLoading()
    {
        var levelNumber = SLS.Data.Game.Level.Value;
        AnalyticsHelper.StartLevel();
        OnLevelStartLoading?.Invoke(levelNumber);
    }

    private void InvokeOnLevelLoaded(int sceneId)
    {
        OnLevelLoaded?.Invoke(sceneId);
    }

    private void UnloadLevel(bool nextLvl)
    {
        if (nextLvl)
        {
            SLS.Data.Game.Level.Value++;
        }
    }

    public void LevelEnd(bool playerWin)
    {
        if (playerWin)
        {
            AnalyticsHelper.CompleteLevel();
        }
        else
        {
            AnalyticsHelper.FailLevel();
        }

        OnLevelEnd?.Invoke(playerWin);
    }

    public void NextLevel()
    {
        UnloadLevel(true);
        LoadLevel();
    }

    public void RestartLevel()
    {
        UnloadLevel(false);
        LoadLevel();
    }
}