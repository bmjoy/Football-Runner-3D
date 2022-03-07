using TFPlay.UI;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoSingleton<MenuUI>
{
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private WinUI winUI;
    [SerializeField] private LoseUI loseUI;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private CoinsUI coinsUI;

    private void Start()
    {
        GameC.Instance.OnInitCompleted += Init;
    }

    private void Init()
    {
        GameC.Instance.OnLevelEnd += OnLevelEnd;
        GameC.Instance.OnLevelLoaded += OnLevelLoaded;
    }


    private void OnLevelLoaded(int a)
    {
        ChangeUIState(true);
    }


    private void OnLevelEnd(bool playerWon)
    {
        if (playerWon)
        {
            winUI.Show();
        }
        else
        {
            loseUI.Show();
        }

        ChangeUIState(false);
    }


    public void SetWinUICoins(int coins)
    {
        winUI.SetCoins(coins);
    }


    private void ChangeUIState(bool isOn)
    {
        settingsUI.gameObject.SetActive(isOn);
        levelUI.gameObject.SetActive(isOn);
        coinsUI.gameObject.SetActive(isOn);
    }
}