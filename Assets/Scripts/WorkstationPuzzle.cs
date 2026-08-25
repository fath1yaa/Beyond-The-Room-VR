using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Puzzle 3 - Workstation password.
///
/// The player types a password using an on-screen keypad generated from code.
/// The entry is compared against gameManager.workstationPassword (e.g. "PORT7ECHO").
/// On success: workstationSolved = true, and a "door unlocked" message is shown.
///
/// Setup:
///   - Put this on a WorkstationManager object.
///   - Assign gameManager, displayText, statusText, the keyContainer
///     (a panel with a GridLayoutGroup), and a keyButtonPrefab (Button + TMP_Text).
///   - The keypad is built automatically at Start from 'keyLayout'.
/// </summary>
public class WorkstationPuzzle : MonoBehaviour
{
    [Header("References")]
    public SpyRoomGameManager gameManager;

    [Header("Display")]
    [Tooltip("Shows what the player has typed so far.")]
    public TMP_Text displayText;

    [Tooltip("Shows status messages like ACCESS DENIED.")]
    public TMP_Text statusText;

    [Header("Keypad generation")]
    [Tooltip("Parent object with a GridLayoutGroup where keys are spawned.")]
    public Transform keyContainer;

    [Tooltip("A Button prefab with a TMP_Text child for the key label.")]
    public Button keyButtonPrefab;

    [Tooltip("Characters to create keys for.")]
    public string keyLayout = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    [Header("Messages")]
    [Tooltip("Shown before the player enters anything.")]
    public string promptMessage = "Enter password";

    [Tooltip("Shown on a correct password.")]
    public string successMessage = "ACCESS GRANTED\nDoor unlocked";

    [Tooltip("Shown on a wrong password.")]
    public string failMessage = "ACCESS DENIED";

    private string currentInput = "";

    private void Start()
    {
        BuildKeypad();
        UpdateDisplay();
        SetStatus(promptMessage);
    }

    private void BuildKeypad()
    {
        if (keyContainer == null || keyButtonPrefab == null)
        {
            Debug.LogWarning("WorkstationPuzzle: keyContainer or keyButtonPrefab not set.");
            return;
        }

        foreach (char c in keyLayout)
        {
            Button key = Instantiate(keyButtonPrefab, keyContainer);
            key.gameObject.SetActive(true);

            TMP_Text label = key.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = c.ToString();

            char captured = c;
            key.onClick.AddListener(() => TypeChar(captured));
        }
    }

    public void TypeChar(char c)
    {
        if (gameManager != null && gameManager.workstationSolved)
            return;

        currentInput += c;
        UpdateDisplay();
    }

    public void Backspace()
    {
        if (currentInput.Length > 0)
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
        UpdateDisplay();
    }

    public void Clear()
    {
        currentInput = "";
        UpdateDisplay();
        SetStatus(promptMessage);
    }

    public void Submit()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("WorkstationPuzzle: gameManager not set.");
            return;
        }

        if (currentInput.ToUpper() == gameManager.workstationPassword.ToUpper())
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
        gameManager.workstationSolved = true;
        Debug.Log("Workstation solved! Door unlocked.");
        SetStatus(successMessage);
    }

    private void OnFailure()
    {
        Debug.Log("Wrong password: " + currentInput);
        SetStatus(failMessage);
        currentInput = "";
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = currentInput;
    }

    private void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;
    }
}