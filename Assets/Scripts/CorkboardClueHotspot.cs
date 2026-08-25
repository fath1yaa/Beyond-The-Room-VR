using UnityEngine;

/// <summary>
/// Goes on each individual clue hotspot on the corkboard
/// (sticky note, satellite photo, distractor, etc.).
///
/// When the hotspot's panel opens, this reports its clue type
/// to the shared CorkboardPuzzle so progress can be tracked.
///
/// Setup per hotspot:
///   - HotspotOpenPanel  (opens this clue's panel)
///   - CorkboardClueHotspot (this script)
///   - set 'clueType' to PortClue / GateClue / Distractor
///   - drag the shared CorkboardPuzzle into 'corkboardPuzzle'
/// </summary>
public class CorkboardClueHotspot : MonoBehaviour
{
    [Header("Which clue is this?")]
    public CorkboardPuzzle.ClueType clueType = CorkboardPuzzle.ClueType.Distractor;

    [Header("Reference")]
    public CorkboardPuzzle corkboardPuzzle;

    /// <summary>
    /// Call this when the hotspot's panel opens.
    /// </summary>
    public void ReportToPuzzle()
    {
        if (corkboardPuzzle == null)
        {
            Debug.LogWarning("CorkboardClueHotspot: corkboardPuzzle reference not set on " + name);
            return;
        }

        corkboardPuzzle.ReportClue(clueType);
    }
}