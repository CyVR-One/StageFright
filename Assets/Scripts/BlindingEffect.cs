using UnityEngine;
using UnityEngine.UI;

public class BlindingEffect : MonoBehaviour
{
    public Camera playerCamera;
    public Light intenseLight;
    public Image blindingImage;
    public float blindingThreshold = 0.9f;
    public AnimationCurve blindingCurve;
    public float blindingDuration = 2.0f;

    private float blindingProgress = 0.0f;
    private bool isBlinding = false;

    private void Update()
    {
        // Calculate the angle between the player's view direction and the direction to the light source
        Vector3 lightDirection = (intenseLight.transform.position - playerCamera.transform.position).normalized;
        float dotProduct = Vector3.Dot(playerCamera.transform.forward, lightDirection);

        // Check if the player is looking at the light source
        bool isLookingAtLight = dotProduct > blindingThreshold;

        // Update the blinding progress based on whether the player is looking at the light source
        blindingProgress += (isLookingAtLight ? 1 : -1) * Time.deltaTime / blindingDuration;
        blindingProgress = Mathf.Clamp01(blindingProgress);

        // Calculate the alpha of the blinding image based on the blinding progress and the blinding curve
        float alpha = blindingCurve.Evaluate(blindingProgress);

        // Update the alpha of the blinding image
        Color imageColor = blindingImage.color;
        imageColor.a = alpha;
        blindingImage.color = imageColor;
    }
}
