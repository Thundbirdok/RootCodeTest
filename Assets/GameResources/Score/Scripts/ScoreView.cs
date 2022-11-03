using TMPro;
using UnityEngine;

namespace GameResources.Score.Scripts
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        
        [SerializeField]
        private ScoreHandler scoreHandler;
        
        private void OnEnable()
        {
            scoreHandler.OnCurrentScoreChanged += SetText;
            
            SetText();
        }

        private void OnDisable() => scoreHandler.OnCurrentScoreChanged -= SetText;

        private void SetText() => text.text = scoreHandler.CurrentScore.ToString();
    }
}
