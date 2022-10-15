using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject levelCompleteMenu;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject pauseButton;
    
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        
        gameOverMenu.SetActive(false);
        endText.SetActive(false);
        levelCompleteMenu.SetActive(false);

    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(2f);
        endText.SetActive(true);
        yield return new WaitForSeconds(3f);
        levelCompleteMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }
    
    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    
    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
