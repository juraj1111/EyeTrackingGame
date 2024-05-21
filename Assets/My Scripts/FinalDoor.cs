using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FinalDoor : MonoBehaviour
{
    public LocomotionSystem LocomotionSystem;
    public GameObject canvas;
    private int currentGemAmmount = 0;
    [SerializeField] private int finalGemAmmount = 4;

    private void Start()
    {
        canvas.SetActive(false);
    }

    public void addGem()
    {
        currentGemAmmount++;
        if(currentGemAmmount == finalGemAmmount)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("open");
            LocomotionSystem.enabled = false;
            canvas.SetActive (true);
        }
    }

    public void removeGem()
    {
        currentGemAmmount--;
    }
}
