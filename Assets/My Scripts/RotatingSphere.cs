using UnityEngine;

public class RotatingSphere : MonoBehaviour
{
    [SerializeField] private float radius = 4f; 
    [SerializeField] private float rotationSpeed = 2f;

    private Vector3 centerPosition;
    private float angle = 0f;
    private bool start = false;

    void Update()
    {
        if (start == false) return;
        angle -= rotationSpeed * Time.deltaTime;

        float x = centerPosition.x + Mathf.Cos(angle) * radius;
        float y = centerPosition.y + Mathf.Sin(angle) * radius;

        // Update the position of the cube
        transform.position = new Vector3(x, y, centerPosition.z);
    }

    public void startRotating(Vector3 vector)
    {
        centerPosition = vector;
        start = true;
    }
}