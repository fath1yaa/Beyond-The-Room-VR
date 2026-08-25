using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CandleHotspot : MonoBehaviour
{
    [Header("References")]
    public CandleLightManager candleLightManager;
    public int candleIndex; // 0, 1, 2, or 3

    [Header("Visuals")]
    public GameObject unlitVisual;   // the unlit candle mesh/particle
    public GameObject litVisual;     // the flame particle or lit candle mesh

    private bool isLit = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(OnSelected);

        // Start unlit
        if (litVisual != null) litVisual.SetActive(false);
        if (unlitVisual != null) unlitVisual.SetActive(true);
    }

    private void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        LightCandle();
    }

    public void LightCandle()
    {
        if (isLit) return;
        isLit = true;

        // Don't hide the candle body, only show the flame
        if (litVisual != null) litVisual.SetActive(true);

        Debug.Log($"Candle {candleIndex} lit!");

        if (candleLightManager != null)
            candleLightManager.ReportCandleLit(candleIndex);
    }
}