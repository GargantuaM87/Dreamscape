using UnityEngine;

public class Button : MonoBehaviour
{
    public void PlaySound(string soundName)
    {
        AudioManager.instance.PlaySound(soundName);
    }
}
