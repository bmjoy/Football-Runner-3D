using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class BaseUIBehaviour : MonoBehaviour
    {
        [SerializeField] private bool hideOnInit;
        [SerializeField] private GameObject content;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            GameC.Instance.OnInitCompleted += Init;
        }

        protected virtual void Init()
        {
            if (hideOnInit)
            {
                Hide();
            }
        }

        public virtual void Show()
        {
            content.SetActive();
        }

        public virtual void Hide()
        {
            content.SetInactive();
        }

        protected void SetButtonInteractible(Button button, bool enable)
        {
            button.interactable = enable;
        }
    }
}
