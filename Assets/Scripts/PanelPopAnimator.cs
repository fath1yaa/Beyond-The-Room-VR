using UnityEngine;
using System.Collections;

/// <summary>
/// Smoothly scales a panel up ("pop"/grow) when it becomes active,
/// so a note on the board feels like it expands into a readable panel.
///
/// Attach this to the panel object (e.g. Panel_PortClue).
/// It animates scale every time the object is enabled.
/// </summary>
public class PanelPopAnimator : MonoBehaviour
{
    [Header("Pop settings")]
    [Tooltip("How long the grow animation takes, in seconds.")]
    public float duration = 0.3f;

    [Tooltip("The scale the panel should end at (its normal full size).")]
    public Vector3 targetScale = new Vector3(0.001f, 0.001f, 0.001f);

    [Tooltip("Scale it starts from (near zero = grows from nothing).")]
    public Vector3 startScale = Vector3.zero;

    [Tooltip("Adds a slight overshoot bounce for a livelier pop.")]
    public bool overshoot = true;

    private Coroutine popRoutine;

    private void OnEnable()
    {
        // Restart the pop every time the panel is shown.
        if (popRoutine != null)
            StopCoroutine(popRoutine);

        popRoutine = StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        float elapsed = 0f;
        transform.localScale = startScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Ease out, with optional overshoot for a bouncy pop.
            float eased = overshoot ? EaseOutBack(t) : EaseOutCubic(t);

            transform.localScale = Vector3.LerpUnclamped(startScale, targetScale, eased);
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private float EaseOutCubic(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3f);
    }

    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }
}