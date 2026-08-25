using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Murder Scene — Grabbable Clue Item
///
/// Attach this to any grabbable evidence object (knife, phone, document, etc.)
/// alongside an XRGrabInteractable.
///
/// Behaviour:
///   - Player grabs the object  → panel opens and floats beside the held hand
///   - Player releases the object → panel closes
///
/// Setup per object:
///   1. Add XRGrabInteractable (handles the grab physics)
///   2. Add this script (GrabbableClueItem)
///   3. Assign 'panelToOpen'  — the clue info panel for this object
///   4. Assign 'playerCamera' — the Main Camera / XR Origin Camera transform
///   5. Optionally adjust 'panelOffset' (local offset relative to the hand)
///
/// The panel is NOT managed by UIManager here — it lives in world space
/// and follows the hand independently, so other UI panels can coexist.
/// </summary>
[RequireComponent(typeof(XRGrabInteractable))]
public class GrabbableClueItem : MonoBehaviour
{
    [Header("Clue Panel")]
    [Tooltip("The world-space UI panel that shows clue info for this object.")]
    public GameObject panelToOpen;

    [Header("Panel Follow Settings")]
    [Tooltip("Offset from the holding hand's position where the panel hovers.")]
    public Vector3 panelOffset = new Vector3(0.15f, 0.1f, 0.25f);

    [Tooltip("How quickly the panel smoothly follows the hand (higher = snappier).")]
    public float followSpeed = 12f;

    [Tooltip("Camera used to make the panel always face the player.")]
    public Transform playerCamera;

    // ── Private state ────────────────────────────────────────────────────────

    private XRGrabInteractable grabInteractable;
    private Transform holdingHand;   // set while grabbed, null while not
    private bool isHeld = false;

    // ── Unity lifecycle ──────────────────────────────────────────────────────

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void Update()
    {
        if (!isHeld || panelToOpen == null || holdingHand == null)
            return;

        // Move panel to hand position + local offset
        Vector3 targetPos = holdingHand.position
                          + holdingHand.right * panelOffset.x
                          + holdingHand.up * panelOffset.y
                          + holdingHand.forward * panelOffset.z;

        panelToOpen.transform.position = Vector3.Lerp(
            panelToOpen.transform.position,
            targetPos,
            Time.deltaTime * followSpeed
        );

        // Always face the player camera
        if (playerCamera != null)
        {
            Vector3 lookDir = panelToOpen.transform.position - playerCamera.position;
            if (lookDir != Vector3.zero)
            {
                panelToOpen.transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }
    }

    // ── Grab / Release callbacks ─────────────────────────────────────────────

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        holdingHand = args.interactorObject.transform;
        isHeld = true;

        if (panelToOpen != null)
        {
            // Snap to hand immediately before smooth follow takes over
            panelToOpen.transform.position = holdingHand.position
                + holdingHand.right * panelOffset.x
                + holdingHand.up * panelOffset.y
                + holdingHand.forward * panelOffset.z;

            panelToOpen.SetActive(true);
        }

        // Optional: report to a murder scene puzzle tracker
        MurderClueHotspot clue = GetComponent<MurderClueHotspot>();
        if (clue != null)
        {
            clue.ReportToPuzzle();
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;
        holdingHand = null;

        if (panelToOpen != null)
        {
            panelToOpen.SetActive(false);
        }
    }
}