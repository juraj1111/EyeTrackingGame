using UnityEngine;

public class DisablePhysics : MonoBehaviour
{
    void Start()
    {
        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            Destroy(rb);
        }
        foreach (var collider in FindObjectsOfType<Collider>())
        {
            Destroy(collider);
        }
    }

}