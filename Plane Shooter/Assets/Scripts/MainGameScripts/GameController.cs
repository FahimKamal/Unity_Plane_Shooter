using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] [CanBeNull] private GameObject pauseMenu;
    [SerializeField] [CanBeNull] private GameObject gameOverMenu;
    [SerializeField] [CanBeNull] private GameObject levelCompleteMenu;
    [SerializeField] [CanBeNull] private GameObject pauseButton;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(true);
        
        if (gameOverMenu != null) gameOverMenu.SetActive(false);
        if (levelCompleteMenu != null) levelCompleteMenu.SetActive(false);

    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GameOver()
    {
        if (gameOverMenu != null) gameOverMenu.SetActive(true);
        if (pauseButton != null) pauseButton.SetActive(false);
    }
    
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void NextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void PauseGame()
    {
        if (pauseMenu != null) pauseMenu.SetActive(true);
        if (pauseButton != null) pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
    
    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
