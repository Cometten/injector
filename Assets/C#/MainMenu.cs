using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    public void PlayGame ()
    {
        
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        
        Application.Quit();
    }

}
