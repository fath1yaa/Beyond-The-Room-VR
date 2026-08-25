using UnityEngine;

public class PhonePuzzle : MonoBehaviour
{
    public GameObject phoneClue;

    public void OpenPhone()
    {
        phoneClue.SetActive(true);

        Debug.Log("PHONE OPENED");
    }
}