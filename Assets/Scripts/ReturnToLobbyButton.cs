using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Return To Lobby Button.
///
/// Attach this to any GameObject that has an XRSimpleInteractable.
/// When the player selects it, they are sent back to the Lobby scene.
///
/// Setup:
///   1. Create a button GameObject in your scene (e.g. a cube or panel).
///   2. Add XRSimpleInteractable to it.
///   3. Attach this script to the same GameObject.
///   4. Make sure your Lobby scene name matches lobbySceneName below.
/// </summary>
public class ReturnToLobbyButton : MonoBehaviour
{
    [Header("Scene Name (must match Build Settings exactly)")]
    public string lobbySceneName = "Lobby";

    private XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        if (interactable != null)
            interactable.selectEntered.AddListener(OnSelected);
        else
            Debug.LogWarning("ReturnToLobbyButton: No XRSimpleInteractable found on " + name);
    }

    private void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        ReturnToLobby();
    }

    public void ReturnToLobby()
    {
        Debug.Log("ReturnToLobbyButton: Returning to lobby -> " + lobbySceneName);
        SceneManager.LoadScene(lobbySceneName);
    }
}