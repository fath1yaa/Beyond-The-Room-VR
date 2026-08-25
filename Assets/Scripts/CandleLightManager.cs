using UnityEngine;

public class CandleLightManager : MonoBehaviour
{
    [Header("References")]
    public HorrorGameManager gameManager;

    [Header("How many candles in the scene")]
    public int totalCandles = 4;

    [Header("Reveal when all candles are lit")]
    public GameObject bowlAndVialsRoot; // parent object containing bowl + all 4 vials

    private bool[] candleLit;
    private int litCount = 0;

    private void Awake()
    {
        candleLit = new bool[totalCandles];

        // Hide bowl and vials at start
        if (bowlAndVialsRoot != null)
            bowlAndVialsRoot.SetActive(false);
    }

    /// <summary>
    /// Called by each CandleHotspot when it is lit.
    /// </summary>
    public void ReportCandleLit(int index)
    {
        if (index < 0 || index >= totalCandles) return;
        if (candleLit[index]) return; // already counted

        candleLit[index] = true;
        litCount++;
        Debug.Log($"Candles lit: {litCount}/{totalCandles}");

        if (litCount >= totalCandles)
            OnAllCandlesLit();
    }

    private void OnAllCandlesLit()
    {
        if (gameManager != null)
            gameManager.allCandlesLit = true;

        if (bowlAndVialsRoot != null)
            bowlAndVialsRoot.SetActive(true);

        Debug.Log("All candles lit! Bowl and vials revealed.");
    }
}