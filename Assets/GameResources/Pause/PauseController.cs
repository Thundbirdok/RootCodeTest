using UnityEngine;

namespace GameResources.Pause
{
    public static class PauseController
    {
        private static float _playTimeScale = 1;
        private const float PAUSE_TIME_SCALE = 0;

        private static bool _isPaused;
        
        public static void Unpause()
        {
            if (_isPaused == false)
            {
                return;
            }

            Time.timeScale = _playTimeScale;

            _isPaused = false;
        }
        
        public static void Pause()
        {
            if (_isPaused)
            {
                return;
            }

            _playTimeScale = Time.timeScale;
            Time.timeScale = PAUSE_TIME_SCALE;

            _isPaused = true;
        }
    }
}
