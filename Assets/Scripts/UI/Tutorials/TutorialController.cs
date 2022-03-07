using System.Collections.Generic;
using UnityEngine;

namespace TFPlay.UI
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private List<TutorialItem> tutorialItems;

        private int currentTutorialNumber;

        private void Start()
        {
            GameC.Instance.OnLevelStartLoading += ShowTutorial;
            InputSystem.Instance.OnTouch += HideTutorial;

            foreach (var tutorialItem in tutorialItems)
            {
                tutorialItem.Stop();
            }
        }

        private void OnDestroy()
        {
            GameC.Instance.OnLevelStartLoading -= ShowTutorial;
            InputSystem.Instance.OnTouch -= HideTutorial;
        }

        private void ShowTutorial(int levelNumber)
        {
            if (levelNumber < tutorialItems.Count)
            {
                currentTutorialNumber = levelNumber;
                tutorialItems[currentTutorialNumber]?.Play();
            }
        }

        private void HideTutorial()
        {
            if (tutorialItems.Count > 0)
            {
                tutorialItems[currentTutorialNumber]?.Stop();
            }
        }
    }
}