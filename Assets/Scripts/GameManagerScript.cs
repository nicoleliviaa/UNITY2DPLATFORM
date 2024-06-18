using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;


    public void gameOver() { 
        gameOverUI.SetActive(true);
    }

    public void restart() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainmenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
