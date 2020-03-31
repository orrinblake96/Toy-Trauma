using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class StartMenuManager : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Debug.Log("Credits Rolling");
        }
    }
}
