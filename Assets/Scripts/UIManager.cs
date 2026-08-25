using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("Camera")]
    public Transform playerCamera;

    [Header("Panels")]
    [Tooltip("Add every panel here. You can add as many as you want.")]
    public List<GameObject> panels = new List<GameObject>();

    [Tooltip("Optional: panel to open automatically when the game starts (e.g. mission brief).")]
    public GameObject startingPanel;

    [Tooltip("Seconds to wait before opening the starting panel, so headset tracking settles.")]
    public float startingPanelDelay = 0.5f;

    private GameObject currentPanel;

    private void Start()
    {
        CloseAllPanels();

        if (startingPanel != null)
        {
            // Delay so the headset has time to establish the real head pose
            // before we place the panel in front of the player.
            Invoke(nameof(OpenStartingPanel), startingPanelDelay);
        }
    }

    private void OpenStartingPanel()
    {
        OpenPanel(startingPanel);
    }

    public void OpenPanel(GameObject panel)
    {
        CloseAllPanels();

        if (panel == null)
            return;

        // Safety: if this panel isn't in the list yet, add it so it's always managed.
        if (!panels.Contains(panel))
        {
            panels.Add(panel);
        }

        currentPanel = panel;
        panel.SetActive(true);
        PlacePanelInFrontOfPlayer(panel);
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        currentPanel = null;
    }

    private void PlacePanelInFrontOfPlayer(GameObject panel)
    {
        if (playerCamera == null)
            return;

        float distance = 1.6f;
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * distance;

        panel.transform.position = targetPosition;
        panel.transform.rotation = Quaternion.LookRotation(panel.transform.position - playerCamera.position);
    }
}