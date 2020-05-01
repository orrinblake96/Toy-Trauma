using System.Collections;
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
        private static readonly int GameOver = Animator.StringToHash("GameOver");

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (playerHealth.currentHealth <= 0)
            {
                _anim.SetTrigger(GameOver);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}
