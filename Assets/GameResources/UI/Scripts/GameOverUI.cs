using System;
using GameResources.GameStateMachine.Scripts;
using GameResources.GameStateMachine.Scripts.States;
using UnityEngine;

namespace GameResources.UI.Scripts
{
    public class GameOverUI : MonoBehaviour, IDependOnState
    {
        public bool IsActiveInThisState => true;
        public Type State => typeof(GameOver);

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
