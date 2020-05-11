using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class StartMenuManager : MonoBehaviour
    {
        public Animator transition;
        
        private static readonly int playGame = Animator.StringToHash("PlayGame");

        public void PlayGame()
        {
            LoadLevel();
        }

        public void PlayClickSound()
        {
            FindObjectOfType<AudioManager>().Play("TV");
        }

        private void LoadLevel()
        {
            
            //Anim
            transition.SetTrigger(playGame);

            //load
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
