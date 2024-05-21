using UnityEngine;
using UnityEngine.InputSystem;

public class GazeInteractable : MonoBehaviour
{
    protected Outline outline; //zvıraznenie objektu pomocou Quick Outline
    public InputActionReference activateAction; //Akcia spúštaèa na ovládaèi

    protected bool isGazed = false;
    protected float distance;
    
    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
    }

    // Aktualizovanie vzdialenosti
    public void gazeInteract(float distance)
    {
        this.distance = distance;
    }

    // Volanie pri prvom pozretí na objekt
    public virtual void onFirstLook(float distance)
    {
        this.distance = distance;
        isGazed = true;
    }

    // Volané keï sa prestane pozera na objekt
    public virtual void gazeInteractEnd()
    {
        isGazed = false;
    }


    public bool getIsGazed()
    {
        return isGazed;
    }

    public float getDistance()
    {
        return distance;
    }
}
