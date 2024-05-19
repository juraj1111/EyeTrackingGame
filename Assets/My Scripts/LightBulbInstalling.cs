using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.XR.Interaction.Toolkit;

public class LightBulbInstalling : MonoBehaviour
{

    public GameObject rotatingSphere;
    public GameObject lightBulbSocket;
    public GameObject spotLight;
    public GameObject pointLight;
    public GameObject canvasSolution;
    private GazeInteractable gazeInteractable;
    private bool startInstallation = false;
    private float distanceFromSocket = 0.1f;
    private float originalZPosition;
    private float progress = 0;
    private bool first = true;
    [SerializeField] public float duration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rotatingSphere.SetActive(false);
        spotLight.SetActive(false);
        pointLight.SetActive(false);
        canvasSolution.SetActive(false);
        gazeInteractable = rotatingSphere.GetComponent<GazeInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startInstallation == false) return;
        if (gazeInteractable.getIsGazed())
        {
            progress = progress + Time.deltaTime;
            float progressDistance = (progress / duration) * distanceFromSocket;
            transform.position = new Vector3(lightBulbSocket.transform.position.x, lightBulbSocket.transform.position.y, originalZPosition + progressDistance);
            transform.Rotate(Vector3.up, 30 * Time.deltaTime);
            if (progress >= duration)
            {
                startInstallation = false;
                rotatingSphere.SetActive(false);
                GetComponent<XRGrabInteractable>().enabled = false;
                spotLight.SetActive(true);
                pointLight.SetActive(true);
                canvasSolution.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lightBulbSocket") && first)
        {
            startInstallation = true;
            GetComponent<XRGrabInteractable>().enabled = false;

            originalZPosition = lightBulbSocket.transform.position.z - distanceFromSocket - 0.07f;
            transform.position = new Vector3(lightBulbSocket.transform.position.x, lightBulbSocket.transform.position.y, originalZPosition);
            transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            GetComponent<Rigidbody>().isKinematic = true;
            rotatingSphere.SetActive(true);
            rotatingSphere.GetComponent<RotatingSphere>().startRotating(transform.position);
            first = false;
        }
    }
}
