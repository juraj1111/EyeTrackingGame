using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float radius = 4f; // Radius of the circular path
    [SerializeField] private float rotationSpeed = 2f; // Speed of rotation

    private Vector3 centerPosition; // Center position of the circular path
    private float angle = 0f; // Angle for circular movement

    void Start()
    {
        // Get the center position of the circular path
        centerPosition = transform.position;
    }

    void Update()
    {
        // Update the angle based on rotation speed and time
        angle += rotationSpeed * Time.deltaTime;

        // Calculate the new position based on the angle and radius
        float x = centerPosition.x + Mathf.Cos(angle) * radius;
        float y = centerPosition.y + Mathf.Sin(angle) * radius;
        float z = centerPosition.z + Mathf.Tan(Mathf.PI / 4) * radius;

        // Update the position of the cube
        transform.position = new Vector3(x, y, z);
    }
}