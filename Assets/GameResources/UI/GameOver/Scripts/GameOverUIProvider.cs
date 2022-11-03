using System;
using GameResources.AddressablesHelper;
using GameResources.GameStateMachine.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameResources.UI.GameOver.Scripts
{
    using GameStateMachine.Scripts.States;
    
    public class GameOverUIProvider : MonoBehaviour, IDependOnState
    {
        public event Action OnPlayAgainClicked;
    
        [SerializeField]
        private AssetReferenceGameObject ui;

        public bool IsActiveInThisState => true;
        
        public Type State => typeof(GameOver);

        private GameOverUI _instance;

        public async void Activate()
        {
            var instance = await AddressablesLoader.Instantiate
            (
                ui, 
                transform, 
                typeof(GameOverUI)
            ) as GameOverUI;

            if (instance == null)
            {
                return;
            }

            _instance = instance;
            
            _instance.OnPlayAgainClicked += InvokeOnPlayAgainClicked;
        }

        public void Deactivate()
        {
            if (_instance == null)
            {
                return;
            }
            
            _instance.OnPlayAgainClicked -= InvokeOnPlayAgainClicked;

            AddressablesLoader.Unload(ui, _instance.gameObject);

            _instance = null;
        }

        private void InvokeOnPlayAgainClicked() => OnPlayAgainClicked?.Invoke();
    }
}
