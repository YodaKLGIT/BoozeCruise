using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI HealthText;

    public static event Action OnPointAdded;

    public static int score = 0;
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
    
    void UpdateScoreCount()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void UpdateHealthCount()
    {
        if (HealthText != null)
        {
            HealthText.text = "Health: " + health.ToString();
        }
    }

    public void GetDamage()
    {
        health--;
        if (health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
        }
    }

    public void AddPoint()
    {
        score++;
        OnPointAdded?.Invoke();
        UpdateScoreCount();
    }
}