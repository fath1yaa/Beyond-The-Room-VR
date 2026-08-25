using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Murder Scene — Suspect Button
///
/// Attach this to each suspect photo object on the board.
/// When the player points and clicks it with the ray interactor,
/// it reports the selection to SuspectBoardPuzzle.
///
/// Setup per suspect photo:
///   1. Add XRSimpleInteractable (enables ray click)
///   2. Add this script
///   3. Set 'suspectID' to A, B, or C
///   4. Drag the shared SuspectBoardPuzzle into 'puzzle'
/// </summary>
[RequireComponent(typeof(XRSimpleInteractable))]
public class SuspectButton : MonoBehaviour
{
    public enum SuspectID { A, B, C }

    [Header("Which suspect is this?")]
    public SuspectID suspectID;

    [Header("Reference")]
    public SuspectBoardPuzzle puzzle;

    private XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnClicked);
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnClicked);
    }

    private void OnClicked(SelectEnterEventArgs args)
    {
        if (puzzle != null)
            puzzle.OnSuspectSelected(suspectID);
    }
}