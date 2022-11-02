using System;
using GameResources.GameStateMachine.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.UI.Scripts
{
    public class PauseUI : MonoBehaviour, IDependOnState
    {
        public event Action OnContinueClicked;
        
        public bool IsActiveInThisState => true;
        public Type State => typeof(GameStateMachine.Scripts.States.Pause);

        [SerializeField]
        private Button continueButton;
        
        public void Activate()
        {
            continueButton.onClick.AddListener(InvokeOnContinueClick);
            
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            
            continueButton.onClick.RemoveListener(InvokeOnContinueClick);
        }
        
        private void InvokeOnContinueClick() => OnContinueClicked?.Invoke();
    }
}
