using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightIntensity : MonoBehaviour
{
    public Transform player;
    public float maxIntensity = 2f;
    public float minIntensity = 0.5f;
    public float lookThreshold = 30f; // The maximum angle difference for the player to be considered "looking at" the light

    private Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        Vector3 toLight = (transform.position - player.position).normalized;
        float angle = Vector3.Angle(player.forward, toLight);

        // Calculate a t value based on how close the angle is to the look threshold
        float t = Mathf.InverseLerp(lookThreshold, 0, angle);

        // Use the t value to interpolate between minIntensity and maxIntensity
        _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}
