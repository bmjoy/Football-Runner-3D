using UnityEngine;

namespace TFPlay.UI
{
    public abstract class TutorialItem : MonoBehaviour
    {
        public virtual void Play()
        {
            gameObject.SetActive();
        }

        public virtual void Stop()
        {
            gameObject.SetInactive();
        }
    }
}