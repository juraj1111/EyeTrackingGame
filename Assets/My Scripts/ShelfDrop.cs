using System.Collections;
using UnityEngine;

public class ShelfDrop : GazeInteractable
{
    private int firstLookCounter = 0;
    private Rigidbody rigidBody;

    protected override void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        if (firstLookCounter == 3)
        {
            if (rigidBody != null)
            {
                //Animator animator = GetComponent<Animator>();
                //animator.SetTrigger("open");
                StartCoroutine(dropShelf());
            }
        }
    }

    public override void onFirstLook(float distance)
    {
        base.onFirstLook(distance);
        firstLookCounter++;
    }

    IEnumerator dropShelf()
    {
        yield return new WaitForSeconds(1f);
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
    }

    

}
