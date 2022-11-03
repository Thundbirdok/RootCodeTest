using TMPro;
using UnityEngine;

namespace GameResources.Score.Scripts
{
    public sealed class BestScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        
        [SerializeField]
        private ScoreHandler scoreHandler;
        
        private void OnEnable()
        {
            scoreHandler.OnBestScoreChanged += SetText;
            
            SetText();
        }

        private void OnDisable() => scoreHandler.OnBestScoreChanged -= SetText;

        private void SetText() => text.text = scoreHandler.BestScore.ToString();
    }
}
