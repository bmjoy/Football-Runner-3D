using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QAConsole : MonoBehaviour
{
    [SerializeField] private Button showQAConsoleButton;
    [SerializeField] private GameObject qaPanel;
    [SerializeField] private TMP_InputField levelInputField;
    [SerializeField] private Button loadLevelButton;

    private bool isOpen = false;

    private void Start()
    {
        loadLevelButton.onClick.AddListener(LoadLevel);
        showQAConsoleButton.onClick.AddListener(OpenConsole);
    }

    private void OpenConsole()
    {
        isOpen = !isOpen;
        qaPanel.SetActive(isOpen);
    }

    private void LoadLevel()
    {
        if (int.TryParse(levelInputField.text, out int levelNumber))
        {
            isOpen = false;
            qaPanel.SetActive(false);
            SLS.Data.Game.Level.Value = levelNumber;
            GameC.Instance.LoadLevel();
        }
    }
}