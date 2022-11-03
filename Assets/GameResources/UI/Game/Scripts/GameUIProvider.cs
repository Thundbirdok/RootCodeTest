using System;
using GameResources.AddressablesHelper;
using GameResources.GameStateMachine.Scripts;
using GameResources.Path.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameResources.UI.Game.Scripts
{
    using GameStateMachine.Scripts.States;
    
    public sealed class GameUIProvider : MonoBehaviour, IDependOnState
    {
        public event Action OnStartClicked;
        public event Action OnPauseClicked;
    
        [SerializeField]
        private AssetReferenceGameObject ui;

        [SerializeField]
        private PathController pathControllerA;
        
        [SerializeField]
        private PathController pathControllerB;
        
        public bool IsActiveInThisState => true;
        
        public Type State => typeof(Game);

        private GameUI _instance;

        public async void Activate()
        {
            var instance = await AddressablesLoader.InstantiateAndTryGetComponent
            (
                ui, 
                transform, 
                typeof(GameUI)
            ) as GameUI;

            if (instance == null)
            {
                return;
            }

            _instance = instance;
            
            _instance.Construct(pathControllerA, pathControllerB);
            
            _instance.OnStartClicked += InvokeOnStartClick;
            _instance.OnPauseClicked += InvokePauseClick;
        }

        public void Deactivate()
        {
            if (_instance == null)
            {
                return;
            }
            
            _instance.OnStartClicked -= InvokeOnStartClick;
            _instance.OnPauseClicked -= InvokePauseClick;
            
            AddressablesLoader.Unload(ui, _instance.gameObject);

            _instance = null;
        }

        private void InvokeOnStartClick() => OnStartClicked?.Invoke();

        private void InvokePauseClick() => OnPauseClicked?.Invoke();
    }
}
