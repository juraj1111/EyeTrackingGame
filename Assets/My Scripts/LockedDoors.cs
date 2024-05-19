using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockedDoors : MonoBehaviour
{
    private XRGrabInteractable doorHandle;

    void Start()
    {
        doorHandle = GetComponent<XRGrabInteractable>();
        doorHandle.enabled = false;
    }

    public void unlock()
    {
        doorHandle.enabled = true;
    }
}


