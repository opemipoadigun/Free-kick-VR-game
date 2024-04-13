using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score = 0; // Initial score value

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        // Initialize the score text
        UpdateScoreText();
    }

    // You can call this method whenever something happens (e.g., block broken)
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; // Add the given score to the total score
        UpdateScoreText(); // Update the displayed score
    }

    // Update the text to display the current score
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Update the text with the current score
        }
    }

    // You can call this method to reset the score to zero
    public void ResetScore()
    {
        score = 0; // Reset the score
        UpdateScoreText(); // Update the displayed score
    }

    private void Update()
    {
        // Check if the specified Oculus button (Button One) is pressed to reset the score
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            ResetScore(); // Reset the score
        }
    }
}
