using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Television : MonoBehaviour
{
    [SerializeField] public int correctPassword = 1234;
    private int[] correctPasswordDigits;
    private int[] givenPassword = { 0, 0, 0, 0 };
    private bool active = true;
    private bool remoteController = false;

    public GameObject televisionCabinet;

    public bool RemoteController { get => remoteController; set => remoteController = value; }

    void Start()
    {
        correctPasswordDigits = correctPassword.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
    }

    public void add(int digit)
    {
        if (remoteController == false) return;
        givenPassword[digit]++;
        if (givenPassword[digit] == 10)
        {
            givenPassword[digit] = 0;
        }

        // Check if all digits have been entered
        bool allDigitsEntered = true;
        for (int i = 0; i < givenPassword.Length; i++)
        {
            if (givenPassword[i] != correctPasswordDigits[i])
            {
                allDigitsEntered = false;
                break;
            }
        }

        if (allDigitsEntered)
        {
            Animator animator = televisionCabinet.GetComponent<Animator>();
            animator.SetTrigger("open");
            active = false;
        }
        else
        {
            active = true;
        }
    }

    public bool isActive()
    {
        return active;
    }

    public void holdRemoteController()
    {

        remoteController = !remoteController;
    }
}
