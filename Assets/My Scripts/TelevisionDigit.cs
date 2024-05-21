using TMPro;
using UnityEngine;

public class TelevisionDigit : GazeInteractable
{
    public Television television;
    public TextMeshPro textMeshProObject;
    [SerializeField] private int digit;
    private int number = 0;
    

    protected override void Start()
    {
        base.Start();
        textMeshProObject.text = number.ToString();
    }

    void Update()
    {
        if (isGazed && television.isActive() && television.RemoteController)
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
            outline.enabled = false;
        }
        else
        {
            outline.enabled = false;
        }
    }

    
}
