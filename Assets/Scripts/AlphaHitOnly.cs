using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Makes a UI Image only clickable on its visible (non-transparent) pixels.
/// Put this on each pie-slice color button so the transparent corners of its
/// rect don't steal clicks from neighboring slices.
///
/// REQUIREMENT: the slice sprite must have "Read/Write Enabled" ticked
/// in its import settings, and have transparency (alpha) around the slice.
/// </summary>
[RequireComponent(typeof(Image))]
public class AlphaHitOnly : MonoBehaviour
{
    [Tooltip("Pixels with alpha below this are NOT clickable (0..1). 0.1 is a good start.")]
    [Range(0f, 1f)]
    public float alphaThreshold = 0.1f;

    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }
}