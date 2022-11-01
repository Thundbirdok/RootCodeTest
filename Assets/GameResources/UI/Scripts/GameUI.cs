using System;
using GameResources.GameStateMachine.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.UI.Scripts
{
    public class GameUI : MonoBehaviour, IDependOnState
    {
        public event Action OnStartClicked;
        
        public bool IsActiveInThisState => true;
        public Type State => typeof(Game);

        [SerializeField]
        private Button button;
        
        public void Activate()
        {
            button.onClick.AddListener(InvokeOnStartClick);
            
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            
            button.onClick.RemoveListener(InvokeOnStartClick);
        }

        private void InvokeOnStartClick()
        {
            OnStartClicked?.Invoke();
        }
    }
}
