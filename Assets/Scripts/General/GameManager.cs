using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Only a temp script for gamejam playtests, will have a much more comprehensive death loop in later development stages
    [SerializeField] private GameObject tempDeathMenu;

    public void RestartStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeathMenu()
    {
        Time.timeScale = 0;
        tempDeathMenu.SetActive(true);
    }

}
