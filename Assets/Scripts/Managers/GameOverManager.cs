﻿using System.Collections;
using Player;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public float restartDelay = 5f;

        private Animator _anim;
        private float _restartTimer;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (playerHealth.currentHealth <= 0)
            {
                _anim.SetTrigger("GameOver");
                _restartTimer += Time.deltaTime;
                if (_restartTimer >= restartDelay) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
