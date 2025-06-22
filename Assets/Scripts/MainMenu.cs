using UnityEngine;

public class MainMenu : MonoBehaviour
{
  
    //Clean up and correct this code later
    void Start()
    {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlaySound("Sophistry");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
