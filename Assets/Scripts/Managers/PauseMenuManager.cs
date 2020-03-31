using System;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class PauseMenuManager : MonoBehaviour
    {
        public static bool gameIsPaused = false;

        public GameObject pauseMenuUi;
        public GameObject playerShooting;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Resume()
        {
            playerShooting.SetActive(true);
            Time.timeScale = 1.0f;
            pauseMenuUi.SetActive(false);
            gameIsPaused = false; 
        }

        private void Pause()
        {
            playerShooting.SetActive(false);
            Time.timeScale = 0.0f;
            pauseMenuUi.SetActive(true);
            gameIsPaused = true;
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("StartMenu");
            Time.timeScale = 1.0f;
            playerShooting.SetActive(true);
        }

        public void QuitGame()
        {
            SceneManager.LoadScene("StartMenu");
            Time.timeScale = 1.0f;
            playerShooting.SetActive(true);
        }
    }
}
