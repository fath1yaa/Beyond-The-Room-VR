using UnityEngine;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Puzzle 2 - Server Rack color sequence.
///
/// The player presses colored buttons in order. The puzzle compares the
/// entered sequence against gameManager.serverSequence (e.g. "BLUE-AMBER-BLUE-RED").
/// On success: serverSolved = true, and the ECHO keyword is revealed.
///
/// Setup:
///   - Put this on a ServerRackManager object.
///   - Each color button calls PressColor("BLUE") / ("AMBER") / ("RED") etc.
///   - Assign gameManager, optional feedback text, and optional ECHO reveal panel.
/// </summary>
public class ServerRackPuzzle : MonoBehaviour
{
    [Header("References")]
    public SpyRoomGameManager gameManager;

    [Header("Optional UI")]
    [Tooltip("Shows the current entered sequence / status (optional).")]
    public TMP_Text feedbackText;

    [Tooltip("Panel that reveals the ECHO keyword on success (optional).")]
    public GameObject echoRevealPanel;

    [Tooltip("UIManager, used to show the ECHO panel (optional).")]
    public UIManager uiManager;

    // The sequence the player has entered so far.
    private List<string> entered = new List<string>();

    // The correct sequence, parsed from the game manager (e.g. ["BLUE","AMBER","BLUE","RED"]).
    private string[] correctSequence;

    private void Start()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("ServerRackPuzzle: gameManager reference not set.");
            return;
        }

        // Parse "BLUE-AMBER-BLUE-RED" into an array.
        correctSequence = gameManager.serverSequence.ToUpper().Split('-');
        UpdateFeedback("Enter the sequence...");
    }

    /// <summary>
    /// Called by each color button. Pass the color name, e.g. "BLUE".
    /// </summary>
    public void PressColor(string color)
    {
        if (gameManager == null || gameManager.serverSolved)
            return;

        color = color.ToUpper();
        entered.Add(color);
        Debug.Log("Pressed: " + color + "  (" + entered.Count + "/" + correctSequence.Length + ")");

        UpdateFeedback(string.Join(" ", entered));

        // Check as soon as we have enough presses.
        if (entered.Count >= correctSequence.Length)
        {
            CheckSequence();
        }
    }

    private void CheckSequence()
    {
        bool correct = true;
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (entered[i] != correctSequence[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            OnSuccess();
        }
        else
        {
            OnFailure();
        }
    }

    private void OnSuccess()
    {
        gameManager.serverSolved = true;
        Debug.Log("Server rack solved! Keyword revealed: ECHO");
        UpdateFeedback("ACCESS GRANTED\nKeyword: ECHO");

        if (uiManager != null && echoRevealPanel != null)
        {
            uiManager.OpenPanel(echoRevealPanel);
        }
    }

    private void OnFailure()
    {
        Debug.Log("Wrong sequence. Resetting.");
        UpdateFeedback("ACCESS DENIED\nTry again.");
        entered.Clear();
    }

    /// <summary>
    /// Optional: hook a Reset button to this to clear input manually.
    /// </summary>
    public void ResetInput()
    {
        entered.Clear();
        UpdateFeedback("Enter the color sequence to unlock the keyword");
    }

    private void UpdateFeedback(string message)
    {
        if (feedbackText != null)
            feedbackText.text = message;
    }
}