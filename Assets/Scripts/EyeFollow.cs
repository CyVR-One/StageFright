using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public GameObject playerHead;
    public float eyeRotationLimit = 45f;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (playerHead != null)
        {
            Vector3 direction = playerHead.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(0, 180, 0); // Add this line
            Quaternion limitedRotation = Quaternion.RotateTowards(initialRotation, targetRotation, eyeRotationLimit);
            transform.rotation = limitedRotation;
        }
    }
}
