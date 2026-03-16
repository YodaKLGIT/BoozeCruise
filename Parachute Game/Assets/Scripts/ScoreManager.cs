using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI HealthText;

    public static int score = 0; // static so other scene can read it
    private int health = 3;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateScoreCount();
        UpdateHealthCount();
    }

    // Update score display
    void UpdateScoreCount()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Update health display
    void UpdateHealthCount()
    {
        if (HealthText != null)
        {
            HealthText.text = "Health: " + health.ToString();
        }
    }

    // Decrease health by 1 and check for game over
    public void GetDamage()
    {
        health--;
        if (health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
        }
    }

    // Increase score by 1
    public void AddPoint()
    {
        score++;
        UpdateScoreCount();
    }
}
