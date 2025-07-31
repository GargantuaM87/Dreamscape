using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Clean up and correct this code later
    void Start()
    {
        AudioManager.instance.PlaySound("Sophistry");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Limbo");
        AudioManager.instance.StopSound();
    }
}
