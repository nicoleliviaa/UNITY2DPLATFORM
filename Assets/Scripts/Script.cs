using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Method to load Level 1
    public void LoadLevel1()
    {
        SceneManager.LoadScene("LEVEL1"); // Make sure the scene name matches the name of your Level 1 scene
    }
}

