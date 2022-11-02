using System;
using GameResources.GameStateMachine.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.UI.Scripts
{
    public class GameOverUI : MonoBehaviour, IDependOnState
    {
        public event Action OnPlayAgainClicked;
        
        public bool IsActiveInThisState => true;
        public Type State => typeof(GameOver);

        [SerializeField]
        private Button playAgain;
        
        public void Activate()
        {
            playAgain.onClick.AddListener(InvokeOnPlayAgain);
            
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            playAgain.onClick.RemoveListener(InvokeOnPlayAgain);
            
            gameObject.SetActive(false);
        }

        private void InvokeOnPlayAgain() => OnPlayAgainClicked?.Invoke();
    }
}
