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
        public bool gamePaused = false;

        public GameObject pauseMenuUi;
        public GameObject playerShooting;
        public GameObject player;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || player.GetComponent<PlayerHealth>().currentHealth <= 0) return;
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("TV");
                Pause();
            }
        }

        public void Resume()
        {
            FindObjectOfType<AudioManager>().Play("TV");
            playerShooting.SetActive(true);
            Time.timeScale = 1.0f;
            pauseMenuUi.SetActive(false);
            gameIsPaused = false;
            gamePaused = false;
        }

        private void Pause()
        {
            playerShooting.SetActive(false);
            Time.timeScale = 0.0f;
            pauseMenuUi.SetActive(true);
            gameIsPaused = true;
            gamePaused = true;
        }

        public void LoadMenu()
        {
            FindObjectOfType<AudioManager>().Play("TV");
            SceneManager.LoadScene("StartMenu");
            Time.timeScale = 1.0f;
            playerShooting.SetActive(true);
        }

        public void QuitGame()
        {
            FindObjectOfType<AudioManager>().Play("TV");
            SceneManager.LoadScene("StartMenu");
            Time.timeScale = 1.0f;
            playerShooting.SetActive(true);
        }
    }
}
