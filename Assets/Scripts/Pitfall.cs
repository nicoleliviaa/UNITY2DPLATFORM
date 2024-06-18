using UnityEngine;
using UnityEngine.SceneManagement;

public class Pitfall : MonoBehaviour
{
    public GameManagerScript gameManagerScript;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManagerScript.gameOver();
        }
    }


}

