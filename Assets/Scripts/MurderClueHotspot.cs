using UnityEngine;

/// <summary>
/// Murder Scene — Clue Tracker
///
/// Parallel to CorkboardClueHotspot but for the murder scene's grabbable evidence.
///
/// Attach this alongside GrabbableClueItem on each evidence object.
/// Set 'clueType' to identify which piece of evidence this is.
/// Drag the shared MurderScenePuzzle manager into 'murderPuzzle'.
///
/// The GrabbableClueItem will call ReportToPuzzle() automatically when grabbed.
/// </summary>
public class MurderClueHotspot : MonoBehaviour
{
    [Header("Which clue is this?")]
    public MurderScenePuzzle.ClueType clueType = MurderScenePuzzle.ClueType.Distractor;

    [Header("Reference")]
    public MurderScenePuzzle murderPuzzle;

    /// <summary>
    /// Called by GrabbableClueItem when this object is first grabbed.
    /// </summary>
    public void ReportToPuzzle()
    {
        if (murderPuzzle == null)
        {
            Debug.LogWarning("MurderClueHotspot: murderPuzzle reference not set on " + name);
            return;
        }

        murderPuzzle.ReportClue(clueType);
    }
}