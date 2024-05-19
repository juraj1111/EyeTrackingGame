using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeExample : GazeInteractable
{
    protected override void Start()
    {
        base.Start();
        outline.enabled = false;
    }

    public override void onFirstLook(float distance)
    {
        outline.enabled = true;
    }
    public override void gazeInteractEnd()
    {
        outline.enabled = false;
    }
}
