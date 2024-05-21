using System.Collections;
using UnityEngine;

public class ShelfDrop : GazeInteractable
{
    private int firstLookCounter = 0;
    private Rigidbody rigidBody;
    [SerializeField] public int numberOFLooks = 3;

    protected override void Start()
    {
        base.Start();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (firstLookCounter == numberOFLooks)
        {
            if (rigidBody != null)
            {
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
