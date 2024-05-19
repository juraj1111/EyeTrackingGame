using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbInstalling : MonoBehaviour
{

    public GameObject rotatingSphere;
    public GameObject lightBulbSocket;
    private bool startInstallation = false;

    //private float progress = 0;
    [SerializeField] public float duration = 15.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rotatingSphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startInstallation)
        {
            //progress = progress + rotatingSphere.getProgress();
            //transform.rotation.y
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("lightBulbSocket"))
        {
            rotatingSphere.SetActive(true);
            startInstallation = true;
        }
    }
}
