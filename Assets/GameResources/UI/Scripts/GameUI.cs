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
        public event Action OnPauseClicked;
        
        public bool IsActiveInThisState => true;
        public Type State => typeof(Game);

        [SerializeField]
        private Button start;

        [SerializeField]
        private Button pause;
        
        public void Activate()
        {
            start.onClick.AddListener(InvokeOnStartClick);
            pause.onClick.AddListener(InvokePauseClick);
            
            start.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            
            start.onClick.RemoveListener(InvokeOnStartClick);
            pause.onClick.RemoveListener(InvokePauseClick);
        }

        private void InvokeOnStartClick()
        {
            start.gameObject.SetActive(false);
            
            OnStartClicked?.Invoke();
        }

        private void InvokePauseClick() => OnPauseClicked?.Invoke();
    }
}
