using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.UI.GameOver.Scripts
{
    public class GameOverUI : MonoBehaviour
    {
        public event Action OnPlayAgainClicked;

        [SerializeField]
        private Button playAgain;
        
        private void OnEnable()
        {
            playAgain.onClick.AddListener(InvokeOnPlayAgain);
            
            gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            playAgain.onClick.RemoveListener(InvokeOnPlayAgain);
            
            gameObject.SetActive(false);
        }

        private void InvokeOnPlayAgain() => OnPlayAgainClicked?.Invoke();
    }
}
