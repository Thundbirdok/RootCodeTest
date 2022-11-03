using System;
using GameResources.AddressablesHelper;
using GameResources.GameStateMachine.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameResources.UI.Pause.Scripts
{
    using GameStateMachine.Scripts.States;
    
    public sealed class PauseUIProvider : MonoBehaviour, IDependOnState
    {
        public event Action OnContinueClicked;
    
        [SerializeField]
        private AssetReferenceGameObject ui;

        public bool IsActiveInThisState => true;
        
        public Type State => typeof(Pause);

        private PauseUI _instance;

        public async void Activate()
        {
            var instance = await AddressablesLoader.InstantiateAndTryGetComponent
            (
                ui, 
                transform, 
                typeof(PauseUI)
            ) as PauseUI;

            if (instance == null)
            {
                return;
            }

            _instance = instance;
            
            _instance.OnContinueClicked += InvokeOnContinueClicked;
        }

        public void Deactivate()
        {
            if (_instance == null)
            {
                return;
            }
            
            _instance.OnContinueClicked -= InvokeOnContinueClicked;

            AddressablesLoader.Unload(ui, _instance.gameObject);

            _instance = null;
        }

        private void InvokeOnContinueClicked() => OnContinueClicked?.Invoke();
    }
}
