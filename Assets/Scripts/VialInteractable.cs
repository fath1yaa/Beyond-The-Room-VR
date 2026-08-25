using UnityEngine;

public class VialInteractable : MonoBehaviour
{
    [Header("Which vial is this?")]
    public BowlVialPuzzle.VialType vialType;

    [Header("Reference to the bowl puzzle")]
    public BowlVialPuzzle bowlPuzzle;

    private bool hasBeenPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenPlaced) return;

        // Check if we entered the bowl's trigger zone
        if (other.CompareTag("Bowl"))
        {
            hasBeenPlaced = true;
            Debug.Log($"Vial '{vialType}' dropped into bowl.");

            if (bowlPuzzle != null)
                bowlPuzzle.OnVialPlaced(vialType);
        }
    }
}