using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Clean up and correct this code later
    void Start()
    {
        AudioManager.instance.PlaySound("Sun Is A Woman");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Limbo");
        AudioManager.instance.StopSound();
    }
}
