using System;
using Player;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Managers
{
    public class SlowTimeManager : MonoBehaviour
    {
        public float slowdownFactor = 0.05f;
        public float slowdownLength = 4f;
        public bool slowtime = false;
        public bool gameRunning = true;
        
        private AudioSource[] _sources;
        private PlayerKillstreak _currentStreak;
        private PauseMenuManager _pauseMenuManager;
        private GameObject _hourglassUi;

        private void Start()
        {
            _sources = GameObject.Find("AudioManager").GetComponents<AudioSource>();
            _currentStreak = GameObject.Find("KillstreakCounter").GetComponent<PlayerKillstreak>();
            _pauseMenuManager = GameObject.Find("PauseCanvas").GetComponent<PauseMenuManager>();
            _hourglassUi = GameObject.Find("UI/PowerUpsCanvas/Hourglass");
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && slowtime)
            {
                slowtime = false;
                SlowMotion();
                _hourglassUi.SetActive(false);
            }

            if (!_pauseMenuManager.gamePaused && Time.timeScale != 1f)
            {
                Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            
                foreach (AudioSource clip in _sources)
                {
                    var pitch = clip.pitch;
                    pitch += (1f/ slowdownLength) * Time.unscaledDeltaTime;
                    clip.pitch = pitch;
                    clip.pitch = Mathf.Clamp(pitch, 0f, 1f);
                } 
            }
            
        }

        public void ShowHourglassUi()
        {
            _hourglassUi.SetActive(true);
        }

        void SlowMotion()
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            
            foreach (AudioSource clip in _sources)
            {
                clip.pitch /= 4;
            }
        }
    }
}
