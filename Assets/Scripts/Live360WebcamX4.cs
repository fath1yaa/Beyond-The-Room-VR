using UnityEngine;

public class Live360WebcamX4 : MonoBehaviour
{
    [SerializeField] private string deviceName = "OBS Virtual Camera";
    private WebCamTexture camTexture;
    private Renderer sphereRenderer;
    private float retryTimer = 0f;
    private const float retryCooldown = 2f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        StartCamera();
    }

    void StartCamera()
    {
        if (camTexture != null)
        {
            camTexture.Stop();
            camTexture = null;
        }

        camTexture = new WebCamTexture(deviceName, 2880, 1440, 30);
        sphereRenderer.material.mainTexture = camTexture;
        camTexture.Play();
        Debug.Log("Camera started: " + deviceName);
    }

    void Update()
    {
        if (camTexture != null && !camTexture.isPlaying)
        {
            retryTimer += Time.deltaTime;
            if (retryTimer >= retryCooldown)
            {
                retryTimer = 0f;
                Debug.LogWarning("Camera stopped, restarting...");
                camTexture.Play();
            }
        }
    }

    void OnDestroy()
    {
        if (camTexture != null && camTexture.isPlaying)
            camTexture.Stop();
    }
}