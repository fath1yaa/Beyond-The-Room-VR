using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HotspotOpenPanel : MonoBehaviour
{
    public UIManager uiManager;
    public GameObject panelToOpen;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelected);
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelected);
        }
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        OpenPanel();
    }

    public void OpenPanel()
    {
        if (uiManager != null && panelToOpen != null)
        {
            uiManager.OpenPanel(panelToOpen);

            // If this hotspot is a corkboard clue, report it
            CorkboardClueHotspot clue = GetComponent<CorkboardClueHotspot>();
            if (clue != null)
            {
                clue.ReportToPuzzle();
            }
        }
    }
}