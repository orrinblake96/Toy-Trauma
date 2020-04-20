using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class SlowTimeManager : MonoBehaviour
    {
        public float slowdownFactor = 0.05f;
        public float slowdownLength = 2f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SlowMotion();
            }

            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

        void SlowMotion()
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}
