using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private float timePlayed = 0f; // Initial time played value

    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();

        // Initialize the time text
        UpdateTimeText();
    }

    // Update the time played value
    void Update()
    {
        if (Time.timeScale != 0) // Only update time if the game is not paused
        {
            timePlayed += Time.deltaTime; // Add the time since the last frame to the total time played
            UpdateTimeText(); // Update the displayed time
        }
    }

    // Update the text to display the current time played
    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            // Format the time played as hours:minutes:seconds
            string formattedTime = FormatTime(timePlayed);
            timeText.text = "Time Played: " + formattedTime; // Update the text with the current time played
        }
    }

    // Format the time in hours:minutes:seconds
    private string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // You can call this method to reset the time played to zero
    public void ResetTimePlayed()
    {
        timePlayed = 0f; // Reset the time played
        UpdateTimeText(); // Update the displayed time
    }

    // You can call this method to pause the time played tracking
    public void PauseTime()
    {
        Time.timeScale = 0f; // Pause the time scale
    }

    // You can call this method to resume the time played tracking
    public void ResumeTime()
    {
        Time.timeScale = 1f; // Resume the time scale
    }
}
