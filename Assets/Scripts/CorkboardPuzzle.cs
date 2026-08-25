using UnityEngine;

/// <summary>
/// Puzzle 1 - Corkboard Intelligence (multi-clue version).
///
/// The player gathers clues from several hotspots on the corkboard.
/// Two clues are REAL and required to learn PORT7:
///   - PortClue  -> keyword PORT
///   - GateClue  -> digit 7
/// Other hotspots can be Distractor (red herrings) that don't count.
///
/// Place ONE CorkboardPuzzle on a manager-style object (e.g. the corkboard root).
/// Each clue hotspot calls ReportClue(...) with its clue type when its panel opens.
/// Once BOTH real clues are seen, corkboardInspected flips to true.
/// </summary>
public class CorkboardPuzzle : MonoBehaviour
{
    public enum ClueType
    {
        PortClue,   // reveals keyword PORT
        GateClue,   // reveals digit 7
        Distractor  // red herring, does not count
    }

    [Header("References")]
    public SpyRoomGameManager gameManager;

    [Header("Progress (read-only, for debugging)")]
    [SerializeField] private bool portClueSeen = false;
    [SerializeField] private bool gateClueSeen = false;

    /// <summary>
    /// Called by each clue hotspot when its panel is opened.
    /// </summary>
    public void ReportClue(ClueType clue)
    {
        switch (clue)
        {
            case ClueType.PortClue:
                if (!portClueSeen)
                {
                    portClueSeen = true;
                    Debug.Log("Clue learned: keyword PORT");
                }
                break;

            case ClueType.GateClue:
                if (!gateClueSeen)
                {
                    gateClueSeen = true;
                    Debug.Log("Clue learned: digit 7");
                }
                break;

            case ClueType.Distractor:
                Debug.Log("Distractor clue viewed (not part of the answer).");
                break;
        }

        TryCompleteCorkboard();
    }

    private void TryCompleteCorkboard()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("CorkboardPuzzle: gameManager reference not set.");
            return;
        }

        // Only flip the flag once BOTH real clues have been seen.
        if (portClueSeen && gateClueSeen && !gameManager.corkboardInspected)
        {
            gameManager.corkboardInspected = true;
            Debug.Log("Corkboard complete. Player has learned: PORT7");
        }
    }
}