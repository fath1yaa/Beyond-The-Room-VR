using UnityEngine;
using TMPro;

public class HorrorKeypad : MonoBehaviour
{
    [Header("References")]
    public HorrorGameManager gameManager;
    public UIManager uiManager;
    public GameObject successPanel;
    public GameObject wrongCodePanel;   // optional brief 'wrong code' feedback

    [Header("UI")]
    public TextMeshProUGUI displayText; // shows digits as player types

    private string enteredCode = "";
    private int maxDigits = 4;

    private void Start()
    {
        UpdateDisplay();
    }

    /// <summary>
    /// Call this from each number button's onClick event.
    /// Pass the digit as a string: "2", "4", "7", "9", etc.
    /// </summary>
    public void PressKey(string digit)
    {
        if (enteredCode.Length >= maxDigits) return;
        enteredCode += digit;
        UpdateDisplay();
        Debug.Log($"Keypad input: {enteredCode}");

        if (enteredCode.Length == maxDigits)
            Validate();
    }

    /// <summary>
    /// Deletes the last digit. Wire to a backspace/delete button.
    /// </summary>
    public void PressDelete()
    {
        if (enteredCode.Length == 0) return;
        enteredCode = enteredCode[..^1];
        UpdateDisplay();
    }

    /// <summary>
    /// Clears all input. Wire to a clear/reset button if you have one.
    /// </summary>
    public void PressClear()
    {
        enteredCode = "";
        UpdateDisplay();
    }

    private void Validate()
    {
        if (gameManager == null) return;

        if (enteredCode == gameManager.correctDoorCode)
        {
            gameManager.doorUnlocked = true;
            Debug.Log("Correct code! Horror room complete.");

            if (uiManager != null && successPanel != null)
                uiManager.OpenPanel(successPanel);
        }
        else
        {
            Debug.Log($"Wrong code: {enteredCode}");
            enteredCode = "";
            UpdateDisplay();

            if (uiManager != null && wrongCodePanel != null)
                uiManager.OpenPanel(wrongCodePanel);
        }
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = enteredCode;
    }
}