using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Lobby Scene Manager - handles navigation to each escape room.
///
/// Setup:
///   1. Attach this script to an empty GameObject in the Lobby scene (e.g. "LobbyManager").
///   2. Create 3 button GameObjects in the scene (e.g. cubes or UI panels).
///   3. Add XRSimpleInteractable to each button GameObject.
///   4. Drag each button into the matching slot below (spyRoomButton, horrorButton, murderButton).
///   5. Make sure all 3 scene names are added in File > Build Settings.
///
/// Scene names expected:
///   - SpyRoom
///   - HorrorScene
///   - MurderScene
/// </summary>
public class LobbySceneManager : MonoBehaviour
{
    [Header("Scene Names (must match Build Settings exactly)")]
    public string spyRoomSceneName = "SpyRoom";
    public string horrorSceneName = "HorrorScene";
    public string murderSceneName = "MurderScene";

    [Header("Button GameObjects (must have XRSimpleInteractable)")]
    public GameObject spyRoomButton;
    public GameObject horrorButton;
    public GameObject murderButton;

    // Internal references to the interactable components
    private XRSimpleInteractable spyRoomInteractable;
    private XRSimpleInteractable horrorInteractable;
    private XRSimpleInteractable murderInteractable;

    private void Awake()
    {
        // Spy Room button
        if (spyRoomButton != null)
        {
            spyRoomInteractable = spyRoomButton.GetComponent<XRSimpleInteractable>();
            if (spyRoomInteractable != null)
                spyRoomInteractable.selectEntered.AddListener(OnSpyRoomSelected);
            else
                Debug.LogWarning("LobbySceneManager: spyRoomButton has no XRSimpleInteractable.");
        }

        // Horror button
        if (horrorButton != null)
        {
            horrorInteractable = horrorButton.GetComponent<XRSimpleInteractable>();
            if (horrorInteractable != null)
                horrorInteractable.selectEntered.AddListener(OnHorrorSelected);
            else
                Debug.LogWarning("LobbySceneManager: horrorButton has no XRSimpleInteractable.");
        }

        // Murder Mystery button
        if (murderButton != null)
        {
            murderInteractable = murderButton.GetComponent<XRSimpleInteractable>();
            if (murderInteractable != null)
                murderInteractable.selectEntered.AddListener(OnMurderSelected);
            else
                Debug.LogWarning("LobbySceneManager: murderButton has no XRSimpleInteractable.");
        }
    }

    private void OnDestroy()
    {
        if (spyRoomInteractable != null)
            spyRoomInteractable.selectEntered.RemoveListener(OnSpyRoomSelected);

        if (horrorInteractable != null)
            horrorInteractable.selectEntered.RemoveListener(OnHorrorSelected);

        if (murderInteractable != null)
            murderInteractable.selectEntered.RemoveListener(OnMurderSelected);
    }

    // --- Listener callbacks ---

    private void OnSpyRoomSelected(SelectEnterEventArgs args)
    {
        LoadScene(spyRoomSceneName);
    }

    private void OnHorrorSelected(SelectEnterEventArgs args)
    {
        LoadScene(horrorSceneName);
    }

    private void OnMurderSelected(SelectEnterEventArgs args)
    {
        LoadScene(murderSceneName);
    }

    // --- Scene loading ---

    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("LobbySceneManager: Scene name is empty!");
            return;
        }

        Debug.Log("LobbySceneManager: Loading scene -> " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    // --- Public methods (optional: call from UI buttons or other scripts) ---

    public void GoToSpyRoom() => LoadScene(spyRoomSceneName);
    public void GoToHorror() => LoadScene(horrorSceneName);
    public void GoToMurder() => LoadScene(murderSceneName);
}