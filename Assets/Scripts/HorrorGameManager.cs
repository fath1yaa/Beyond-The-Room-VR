using UnityEngine;

public class HorrorGameManager : MonoBehaviour
{
    [Header("Puzzle Progress (read-only, for debugging)")]
    public bool allCandlesLit = false;
    public bool vialPuzzleSolved = false;
    public bool wallClueRead = false;
    public bool doorUnlocked = false;

    [Header("Answer")]
    public string correctDoorCode = "2479";

    public void SetDoorUnlocked()
    {
        doorUnlocked = true;
        Debug.Log("Horror room complete!");
    }
}