using UnityEngine;
using System.Collections;

/// <summary>
/// Murder Scene — Suspect Board Puzzle (Puzzle 3)
///
/// Place ONE of these on a manager object in the murder scene.
/// 
/// - Player clicks a suspect photo (SuspectButton)
/// - Correct answer → victory panel shown, door unlocked
/// - Wrong answer   → red flash on the chosen photo, then resets
///
/// Setup:
///   1. Set 'correctSuspect' to whichever SuspectID is the murderer
///   2. Drag in the three suspect GameObjects (for the flash effect)
///   3. Drag in the door GameObject and the victory panel
/// </summary>
public class SuspectBoardPuzzle : MonoBehaviour
{
    [Header("Puzzle Answer")]
    [Tooltip("Which suspect is the correct murderer?")]
    public SuspectButton.SuspectID correctSuspect = SuspectButton.SuspectID.B;

    [Header("Suspect Photo Objects")]
    [Tooltip("The photo/card GameObject for Suspect A (needs a Renderer).")]
    public GameObject suspectA_Object;

    [Tooltip("The photo/card GameObject for Suspect B (needs a Renderer).")]
    public GameObject suspectB_Object;

    [Tooltip("The photo/card GameObject for Suspect C (needs a Renderer).")]
    public GameObject suspectC_Object;

    [Header("Scene References")]
    [Tooltip("The door that unlocks on a correct answer.")]
    public GameObject murdersSceneDoor;

    [Tooltip("Victory / success panel to show on correct answer.")]
    public GameObject victoryPanel;

    [Tooltip("Camera used to place the victory panel in front of player.")]
    public Transform playerCamera;

    [Header("Wrong Answer Flash")]
    [Tooltip("How long the red flash lasts in seconds.")]
    public float flashDuration = 0.8f;

    [Tooltip("The red colour used for the wrong-answer flash.")]
    public Color flashColor = new Color(1f, 0.1f, 0.1f, 1f);

    // ── Private ──────────────────────────────────────────────────────────────

    private bool puzzleSolved = false;

    // ── Public entry point ───────────────────────────────────────────────────

    /// <summary>Called by SuspectButton when a photo is clicked.</summary>
    public void OnSuspectSelected(SuspectButton.SuspectID selected)
    {
        if (puzzleSolved) return;

        if (selected == correctSuspect)
        {
            HandleCorrectAnswer();
        }
        else
        {
            GameObject wrongObj = GetSuspectObject(selected);
            if (wrongObj != null)
                StartCoroutine(FlashRed(wrongObj));
        }
    }

    // ── Correct answer ───────────────────────────────────────────────────────

    private void HandleCorrectAnswer()
    {
        puzzleSolved = true;
        Debug.Log("Correct suspect identified: " + correctSuspect);

        // Unlock the door
        if (murdersSceneDoor != null)
        {
            // Simple approach: disable the door so it disappears / opens
            // Replace with your own door animation call if you have one
            murdersSceneDoor.SetActive(false);
        }

        // Show victory panel in front of player
        if (victoryPanel != null && playerCamera != null)
        {
            victoryPanel.SetActive(true);
            float distance = 1.6f;
            victoryPanel.transform.position = playerCamera.position
                                            + playerCamera.forward * distance;
            victoryPanel.transform.rotation = Quaternion.LookRotation(
                victoryPanel.transform.position - playerCamera.position);
        }
    }

    // ── Wrong answer flash ───────────────────────────────────────────────────

    private IEnumerator FlashRed(GameObject target)
    {
        Renderer rend = target.GetComponentInChildren<Renderer>();
        if (rend == null) yield break;

        // Store every original color across all materials
        Color[] originalColors = new Color[rend.materials.Length];
        for (int i = 0; i < rend.materials.Length; i++)
            originalColors[i] = rend.materials[i].color;

        // Flash red
        for (int i = 0; i < rend.materials.Length; i++)
            rend.materials[i].color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // Restore original colors
        for (int i = 0; i < rend.materials.Length; i++)
            rend.materials[i].color = originalColors[i];
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private GameObject GetSuspectObject(SuspectButton.SuspectID id)
    {
        switch (id)
        {
            case SuspectButton.SuspectID.A: return suspectA_Object;
            case SuspectButton.SuspectID.B: return suspectB_Object;
            case SuspectButton.SuspectID.C: return suspectC_Object;
            default: return null;
        }
    }
}