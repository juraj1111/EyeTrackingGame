using UnityEngine;

public class KeypadKey : GazeInteractable
{
    [SerializeField] public float dwellTime = 2.0f;
    [SerializeField] public float beforeDwellTime = 0.2f;
    [SerializeField] public int number;
    public Keypad keypad;
    private float gazeDuration = 0;
    private bool active = true;

    protected override void Update()
    {
        if (isGazed && distance < 0.8 && active == true)
        {
            gazeDuration += Time.deltaTime;
            float progress = Mathf.Clamp01(gazeDuration / dwellTime);

            outline.enabled = true;

            if (gazeDuration >= dwellTime)
            {
                if (number > -1)
                {
                    if (keypad.add(number)) active = false;
                    outline.OutlineColor = Color.blue;
                }
                else keypad.remove();
                gazeDuration = 0;
            }
            else
            {
                outline.OutlineColor = Color.cyan;
            }
        }
        else
        {
            outline.enabled = false;
        }
        
    }

    public override void gazeInteractEnd()
    {
        base.gazeInteractEnd();
        gazeDuration = 0;
    }
}

