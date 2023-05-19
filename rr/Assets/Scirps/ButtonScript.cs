using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour
{
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
            // Aktif sahneyi yeniden yükle
        }
}
