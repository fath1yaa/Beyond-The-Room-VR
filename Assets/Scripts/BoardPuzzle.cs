using UnityEngine;

public class BoardPuzzle : MonoBehaviour
{
    public GameObject boardClue;
    public GameObject escapeDoor;
    public GameObject victoryMessage;

    public void CorrectSuspect()
    {
        boardClue.SetActive(true);

        escapeDoor.SetActive(false);

        victoryMessage.SetActive(true);

        Debug.Log("ESCAPE UNLOCKED");
    }
}