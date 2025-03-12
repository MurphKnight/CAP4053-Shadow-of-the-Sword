using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Manor");
        

    }

    public void QuitGame() { 
        Application.Quit();
    }
}
