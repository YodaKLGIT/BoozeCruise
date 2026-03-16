using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;

    void Start()
    {
        // Show final score
        finalScoreText.text = "Final Score: " + ScoreManager.score;

        // Reset score for the next game
        ScoreManager.score = 0;
    }
}
