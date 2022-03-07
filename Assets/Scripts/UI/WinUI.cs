using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace TFPlay.UI
{
    public class WinUI : BaseEndScreenUI
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private List<ParticleSystem> effects;
        [SerializeField] private TextMeshProUGUI _textCoins;

        protected override void Init()
        {
            base.Init();
            continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        public override void Show()
        {
            base.Show();
            SetButtonInteractible(continueButton, true);
            PlayEffects();
        }

        public override void Hide()
        {
            base.Hide();
            SetButtonInteractible(continueButton, false);
        }

        public void SetCoins(int coins)
        {
            _textCoins.text = "+" + coins.ToString();
        }

        private void OnContinueButtonClicked()
        {
            Hide();
            GameC.Instance.NextLevel();
        }

        private void PlayEffects()
        {
            foreach (var effect in effects)
            {
                effect.Play();
            }
        }
    }
}