using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SafeSensor : GazeInteractable
{
    public GameObject safe;
    private Animator animator;
    [SerializeField] public float interactionDistance = 0.4f;
    protected override void Start()
    {
        outline = GetComponent<Outline>();
        animator = safe.GetComponent<Animator>();
        if (outline == null)
        {
            Debug.LogWarning("Outliner component not found!");
        }
    }

    protected override void Update()
    {
        if (isGazed && distance < interactionDistance)
        {
            outline.enabled = true;
            outline.OutlineColor = Color.red;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public override void onFirstLook(float distance)
    {
        base.onFirstLook(distance);
        if (distance < interactionDistance)
        {
            animator.ResetTrigger("close");
            animator.SetTrigger("open");
        }
    }

    public override void gazeInteractEnd()
    {
        base.gazeInteractEnd();
        if (distance < interactionDistance)
        {
            animator.ResetTrigger("open");
            animator.SetTrigger("close");
        }
    }
}
