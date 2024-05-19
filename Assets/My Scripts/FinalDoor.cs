using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private int currentGemAmmount = 0;
    private int finalGemAmmount = 1;

    public void addGem()
    {
        currentGemAmmount++;
        if(currentGemAmmount == finalGemAmmount)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("open");
        }
    }

    public void removeGem()
    {
        currentGemAmmount--;
    }
}
