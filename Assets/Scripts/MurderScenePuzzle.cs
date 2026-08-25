using UnityEngine;

/// <summary>
/// Murder Scene — Puzzle Manager
///
/// Tracks which pieces of evidence the player has examined (grabbed).
/// Parallel structure to CorkboardPuzzle.
///
/// Place ONE MurderScenePuzzle on a manager object in the murder scene.
/// Each evidence item (knife, phone, note, …) has a MurderClueHotspot that
/// calls ReportClue() the first time the player grabs it.
///
/// Add or remove ClueType entries freely to match your scene's evidence.
/// Only clues NOT marked as Distractor count toward completion.
/// </summary>
public class MurderScenePuzzle : MonoBehaviour
{
    public enum ClueType
    {
        Knife,       // e.g. reveals weapon type
        Phone,       // e.g. reveals last call / message clue
        Note,        // e.g. ransom / threat note
        Distractor   // red herring, does not count toward completion
    }

    [Header("References")]
    public SpyRoomGameManager gameManager;   // reuse the shared game manager, or swap for a MurderSceneGameManager

    [Header("Progress (read-only, for debugging)")]
    [SerializeField] private bool knifeSeen = false;
    [SerializeField] private bool phoneSeen = false;
    [SerializeField] private bool noteSeen = false;

    [Header("Progress Flags")]
    public bool allCluesFound = false;
    public bool suspectIdentified = false;

    /// <summary>
    /// Called by MurderClueHotspot when an evidence item is grabbed for the first time.
    /// </summary>
    public void ReportClue(ClueType clue)
    {
        switch (clue)
        {
            case ClueType.Knife:
                if (!knifeSeen)
                {
                    knifeSeen = true;
                    Debug.Log("Evidence examined: Knife");
                }
                break;

            case ClueType.Phone:
                if (!phoneSeen)
                {
                    phoneSeen = true;
                    Debug.Log("Evidence examined: Phone");
                }
                break;

            case ClueType.Note:
                if (!noteSeen)
                {
                    noteSeen = true;
                    Debug.Log("Evidence examined: Note");
                }
                break;

            case ClueType.Distractor:
                Debug.Log("Distractor evidence grabbed (not part of the answer).");
                break;
        }

        TryCompleteMurderScene();
    }

    private void TryCompleteMurderScene()
    {
        if (knifeSeen && phoneSeen && noteSeen)
        {
            allCluesFound = true;  // ← add this line
            Debug.Log("Murder scene complete — all key evidence examined.");
        }
    }
}