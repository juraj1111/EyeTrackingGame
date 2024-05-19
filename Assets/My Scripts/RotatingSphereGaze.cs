using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSphereGaze : GazeInteractable
{
    // Update is called once per frame
    void Update()
    {
        if(isGazed)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}
