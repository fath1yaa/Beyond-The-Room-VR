using UnityEngine;

public class KnifePuzzle : MonoBehaviour
{
    public GameObject cluePanel;

    private bool solved = false;

    public void SolvePuzzle()
    {
        if (solved) return;

        solved = true;

        cluePanel.SetActive(true);

        Debug.Log("Puzzle 1 Solved");
    }
}