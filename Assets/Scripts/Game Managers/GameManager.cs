using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Only a temp script for gamejam playtests, will have a much more comprehensive death loop in later development stages
    [SerializeField] private GameObject tempDeathMenu;
    [SerializeField] private GameObject pauseMenu;
    private bool isGamePasued = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PauseGame();
        }
    }

    public void RestartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeathMenu()
    {
        Time.timeScale = 0;
        tempDeathMenu.SetActive(true);
    }

    public void PauseGame()
    {
        isGamePasued = !isGamePasued;
        if (isGamePasued == true)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

}
