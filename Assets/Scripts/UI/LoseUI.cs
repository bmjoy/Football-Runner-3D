using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class LoseUI : BaseEndScreenUI
    {
        [SerializeField] private Button restartButton;

        protected override void Init()
        {
            base.Init();
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        public override void Show()
        {
            base.Show();
            SetButtonInteractible(restartButton, true);
        }

        public override void Hide()
        {
            base.Hide();
            SetButtonInteractible(restartButton, false);
        }

        private void OnRestartButtonClicked()
        {
            Hide();
            GameC.Instance.RestartLevel();
        }
    }
}