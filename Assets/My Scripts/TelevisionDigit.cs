using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TelevisionDigit : GazeInteractable
{
    public Television television;
    public TextMeshPro textMeshProObject;
    [SerializeField] private int digit;
    private bool active = true;
    private int number = 0;
    private bool remoteController = false;

    protected override void Start()
    {
        base.Start();
        textMeshProObject.text = number.ToString();
    }

    protected override void Update()
    {
        if (isGazed && television.isActive())
        {
            if (activateAction != null)
            {
                if (activateAction.action.triggered)
                {
                    television.add(digit);
                    number++;
                    if (number == 10) number = 0;
                    textMeshProObject.text = number.ToString();
                }
            }
            outline.enabled = true;
            outline.OutlineColor = Color.yellow;
        }
        else if (!television.isActive())
        {
            textMeshProObject.color = Color.green;
        }
        else
        {
            outline.enabled = false;
        }

    }

    public void holdRemoteController()
    {
        remoteController = !remoteController;
    }
}
