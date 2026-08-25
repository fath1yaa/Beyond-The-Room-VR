using System.Collections.Generic;
using UnityEngine;

public class BowlVialPuzzle : MonoBehaviour
{
    public enum VialType { Lunatic, Executioner, Infant, Sinner }

    [Header("References")]
    public HorrorGameManager gameManager;
    public UIManager uiManager;

    [Header("Reveal on correct solution")]
    public GameObject wallClueObject;       // the wall panel showing numbers 2 4 7 9
    public GameObject wallCluePanel;        // optional: a UI panel popup for the clue

    [Header("Feedback panels (optional)")]
    public GameObject wrongOrderPanel;      // brief 'wrong order' message panel

    private readonly VialType[] correctSequence = { VialType.Sinner, VialType.Lunatic };
    private List<VialType> placedVials = new List<VialType>();
    private bool solved = false;

    /// <summary>
    /// Called by VialInteractable when a vial is dropped into the bowl.
    /// </summary>
    public void OnVialPlaced(VialType vial)
    {
        if (solved) return;

        // Distractors go in but don't count toward the solution
        if (vial == VialType.Executioner || vial == VialType.Infant)
        {
            Debug.Log($"Distractor vial placed: {vial} (ignored for puzzle)");
            return;
        }

        placedVials.Add(vial);
        Debug.Log($"Clue vial placed: {vial} — {placedVials.Count}/{correctSequence.Length}");

        if (placedVials.Count >= correctSequence.Length)
            ValidateSolution();
    }

    private void ValidateSolution()
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (placedVials[i] != correctSequence[i])
            {
                Debug.Log("Wrong vial order! Resetting.");
                placedVials.Clear();

                // Show wrong order feedback if panel is set
                if (uiManager != null && wrongOrderPanel != null)
                    uiManager.OpenPanel(wrongOrderPanel);

                return;
            }
        }

        // Correct!
        solved = true;

        if (gameManager != null)
            gameManager.vialPuzzleSolved = true;

        Debug.Log("Correct vial order! Sinner → Lunatic. Revealing wall clue.");

        // Activate the physical wall clue object in the scene
        if (wallClueObject != null)
            wallClueObject.SetActive(true);

        // Open the clue panel popup if set
        if (uiManager != null && wallCluePanel != null)
            uiManager.OpenPanel(wallCluePanel);
    }
}