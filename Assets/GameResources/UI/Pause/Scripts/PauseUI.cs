using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.UI.Pause.Scripts
{
    public class PauseUI : MonoBehaviour
    {
        public event Action OnContinueClicked;

        [SerializeField]
        private Button continueButton;
        
        private void OnEnable()
        {
            continueButton.onClick.AddListener(InvokeOnContinueClick);
        }

        private void OnDisable()
        {
            continueButton.onClick.RemoveListener(InvokeOnContinueClick);
        }
        
        private void InvokeOnContinueClick() => OnContinueClicked?.Invoke();
    }
}
