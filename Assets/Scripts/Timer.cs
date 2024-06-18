using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 100f;
    [SerializeField] Text countdownText;  // Serialized field to reference the UI Text component
    private PlayerMovement player;  // Assuming these are references to other scripts
    private CoinManager coin;
    public GameManagerScript gameManagerScript;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;  // Time.deltaTime is already in seconds, no need for * 1
        UpdateTimerDisplay();

        if (currentTime <= 0)
        {
            currentTime = 0;
            CheckGameOver();
        }
    }

    void UpdateTimerDisplay()
    {
        countdownText.text = "TIME: " + currentTime.ToString("0");
    }

    void CheckGameOver()
    {
        if (coin != null && coin.GetCoinCount() < 21)
        {
            gameManagerScript.gameOver();  // Call GameOver method on the GameManagerScript
        }
        else
        {
            gameManagerScript.gameOver();  // This line seems redundant if both conditions call GameOver
        }
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
