using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public GameObject playerHead;
    public float spotlightLimit = 45f;

    private void Update()
    {
        if (playerHead != null)
        {
            Vector3 direction = playerHead.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion limitedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, spotlightLimit);
            transform.rotation = limitedRotation;
        }
    }
}
