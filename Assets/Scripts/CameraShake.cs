using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineFreeLook freeLookCam;
    private CinemachineBasicMultiChannelPerlin[] rigNoises;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    void Awake()
    {
        Instance = this;

        freeLookCam = GetComponent<CinemachineFreeLook>();
        if (freeLookCam == null)
        {
            Debug.LogError("CinemachineFreeLook not found on GameObject!");
            return;
        }

        rigNoises = new CinemachineBasicMultiChannelPerlin[3];

        for (int i = 0; i < 3; i++)
        {
            var rig = freeLookCam.GetRig(i); // Get top/mid/bottom rig
            var noise = rig.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (noise == null)
            {
                Debug.LogWarning($"No Perlin noise component on Rig {i}. Did you forget to add Noise?");
            }
            rigNoises[i] = noise;
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;

        foreach (var noise in rigNoises)
        {
            if (noise != null)
                noise.m_AmplitudeGain = intensity;
        }
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                foreach (var noise in rigNoises)
                {
                    if (noise != null)
                        noise.m_AmplitudeGain = 0f;
                }
            }
        }
    }
}
