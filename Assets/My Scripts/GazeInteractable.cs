using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GazeInteractable : MonoBehaviour
{
    protected Outline outline; //outline of the gazed object
    public InputActionReference activateAction; //controller trigger action

    //private bool isGazeInteractable = true;
    private bool activated = false;
    //private bool firstLook = false;
    protected bool isGazed = false;
    protected float distance;
    

    //public bool hasGazeInteraction()
    //{
        //return isGazeInteractable;
    //}

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogWarning("Outliner component not found!");
        }
    }

    protected virtual void Update()
    {
        if (isGazed)
        {
            if(activateAction != null)
            {
                if (activateAction.action.triggered)
                {
                    gazeActivate();
                }
            }
            outline.enabled = true;
            outline.OutlineColor = Color.yellow;
        }
        else
        {
            outline.enabled = false;
        }
    }


    public void gazeInteract(float distance)
    {
        //Debug.Log("Looking at gazeInteraction object!");
        //firstLook = false;
        this.distance = distance;
        //Debug.Log("distance " + distance);

    }

    public virtual void onFirstLook(float distance)
    {
        this.distance = distance;
        //Debug.Log("distance " + distance);
        isGazed = true;
        //firstLook = true;
    }

    public virtual void gazeInteractEnd()
    {
        isGazed = false;
    }

    public void gazeActivate()
    {
        activated = !activated;
    }
}
