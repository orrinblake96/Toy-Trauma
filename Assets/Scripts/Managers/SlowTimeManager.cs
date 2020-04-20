using System;
using Player;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class SlowTimeManager : MonoBehaviour
    {
        public float slowdownFactor = 0.05f;
        public float slowdownLength = 4f;
        
        private AudioSource[] _sources;
        private PlayerKillstreak _currentStreak;

        private void Start()
        {
            _sources = GetComponents<AudioSource>();
            _currentStreak = GameObject.Find("KillstreakCounter").GetComponent<PlayerKillstreak>();
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Q) && _currentStreak.currentKillStreak % 20 == 0)
            {
                SlowMotion();
            }

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
