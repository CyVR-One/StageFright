using UnityEngine;

public class SpotlightFollow : MonoBehaviour
{
    public GameObject playerFoot;
    public float rotationLimit = 45f;

    private void Update()
    {
        if (playerFoot != null)
        {
            Vector3 direction = playerFoot.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion limitedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationLimit);
            transform.rotation = limitedRotation;
        }
    }
}
