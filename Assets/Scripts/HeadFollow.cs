using UnityEngine;

public class HeadFollow : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
    }
}
