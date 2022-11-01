using System;
using System.IO;
using GameResources.Save;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameResources.Score.Scripts
{
    [CreateAssetMenu(fileName = "ScoreHandler", menuName = "Score/ScoreHandler")]
    public class ScoreHandler : ScriptableObject, ISaveProgress
    {
        public event Action OnCurrentScoreChanged;
        public event Action OnBestScoreChanged;
        
        private int _currentScore;

        public int CurrentScore
        {
            get
            {
                return _currentScore;
            }

            private set
            {
                if (_currentScore == value)
                {
                    return;
                }

                _currentScore = value;
                
                if (_currentScore > BestScore)
                {
                    BestScore = _currentScore;
                }
                
                OnCurrentScoreChanged?.Invoke();
            }
        }
        
        private int _bestScore;
        public int BestScore
        {
            get
            {
                if (_isInited == false)
                {
                    Init();
                }

                return _bestScore;
            }

            private set
            {
                _bestScore = value;
                
                OnBestScoreChanged?.Invoke();
            }
        }
        
        private const string FILE_NAME = "BestScore.json";

        private const string BEST_SCORE_KEY = "BestScore";
        
        private static string JsonPath => Application.persistentDataPath + FILE_NAME;
        
        private JObject _jObject;

        private bool _isInited;

        public void AddPoints(int value)
        {
            CurrentScore += value;
        }

        public void ResetCurrentScore()
        {
            _currentScore = 0;
        }

        public void Save()
        {
            var saveJObject = new JObject();

            saveJObject.Add(BEST_SCORE_KEY, _bestScore);
            
            using var file = File.CreateText(JsonPath);
            using JsonTextWriter writer = new(file);
            saveJObject.WriteTo(writer);
        }
        
        private void Init()
        {
            ResetCurrentScore();
            
            GetJObject();

            GetBestScore();

            _isInited = true;
        }

        private void GetJObject()
        {
            try
            {
                using var file = File.OpenText(JsonPath);
                using JsonTextReader reader = new(file);

                _jObject = (JObject)JToken.ReadFrom(reader);
            }
            catch (FileNotFoundException)
            {
                _jObject = new JObject();
            }
        }
        
        private void GetBestScore()
        {
            var token = _jObject.SelectToken(BEST_SCORE_KEY);

            _bestScore = token?.Value<int>() ?? 0;
        }
    }
}
