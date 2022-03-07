using System;
using UnityEngine;
using UnityEngine.UI;

public interface ISceneFaderAnimation
{
    public void Show(Action callback);
    public void Hide(Action callback);
    public void ShowImmediately();
    public void HideImmediately();
}

[RequireComponent(typeof(CanvasGroup), typeof(Image))]
public class SceneFaderContoller : MonoBehaviour
{
    private ISceneFaderAnimation fader;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        fader = GetComponentInChildren<ISceneFaderAnimation>(true);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ShowImmediately();
        GameC.Instance.OnShowFadeUI += GameC_OnLevelStartLoading;
        GameC.Instance.OnLevelLoaded += GameC_OnLevelLoaded;
    }

    private void GameC_OnLevelStartLoading()
    {
        Show();
    }

    private void GameC_OnLevelLoaded(int levelNumber)
    {
        Hide();
    }

    public void Show()
    {
        if (fader == null) return;
        if (canvasGroup == null) return;

        fader.Show(Help);
        canvasGroup.blocksRaycasts = true;
        void Help()
        {
            GameC.Instance.InvokeOnStartLevelLoading();
            //this.WaitAndDoCoroutine(1, () => GameC.Instance.InvokeOnStartLevelLoading());//debug
        }
    }

    public void Hide()
    {
        if (fader == null) return;
        if (canvasGroup == null) return;

        fader.Hide(Help);
        void Help()
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowImmediately()
    {
        if (fader == null) return;
        if (canvasGroup == null) return;

        fader.ShowImmediately();
        canvasGroup.blocksRaycasts = true;
    }

    public void HideImmediately()
    {
        if (fader == null) return;
        if (canvasGroup == null) return;

        fader.HideImmediately();
        canvasGroup.blocksRaycasts = false;
    }
}
