using System.Collections;
using UnityEngine;

public class GazeBallInteraction : MonoBehaviour
{
    public GameObject ballPrefab;
    public Vector3 minBounds = new Vector3(-5, 1, -5);
    public Vector3 maxBounds = new Vector3(5, 5, 5);
    public Quaternion boundsRotation = Quaternion.identity; // Add this line
    public float spawnInterval = 5.0f;
    public LayerMask ballLayer;
    public float gazeDistance = 10.0f;

    private Camera mainCamera;
    private RaycastHit hit;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnBalls());
    }

    private void Update()
    {
        VisualizeGaze();
    }

    IEnumerator SpawnBalls()
    {
        while (true)
        {
            GenerateRandomBall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void GenerateRandomBall()
    {
        Vector3 randomLocalPosition = new Vector3(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            Random.Range(minBounds.z, maxBounds.z)
        );
        
        // Rotate the random position
        Vector3 randomPosition = boundsRotation * randomLocalPosition;
        Instantiate(ballPrefab, randomPosition, Quaternion.identity);
    }

    private void VisualizeGaze()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, gazeDistance, ballLayer))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void CheckGaze()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out hit, gazeDistance, ballLayer))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    // Update the OnDrawGizmos function
    void OnDrawGizmos()
    {
        // Draw the spawn area
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, boundsRotation, Vector3.one);
        Gizmos.DrawCube((minBounds + maxBounds) / 2, maxBounds - minBounds);

        // Draw the gaze ray if the main camera is assigned
        if (mainCamera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * gazeDistance);
        }
    }
}
