using TMPro;
using UnityEngine;

public class KeypadKey : GazeInteractable
{
    [SerializeField] public float dwellTime = 2.0f;
    [SerializeField] public float beforeDwellTime = 0.2f;
    [SerializeField] public int number;
    public TextMeshPro numberText;
    public Keypad keypad;
    private float gazeDuration = 0;
    private bool active = true;
    private TextMeshPro percentageText;

    void Update()
    {
        if (isGazed && distance < 0.8 && active == true)
        {
            gazeDuration += Time.deltaTime;
            float progress = Mathf.Clamp01(gazeDuration / dwellTime);

            percentageText.text = (progress * 100f).ToString("F0") + "%";

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

    public override void onFirstLook(float distance)
    {
        base.onFirstLook(distance);
        if (distance < 0.8)
        {
            percentageText = new GameObject("PercentageText").AddComponent<TextMeshPro>();
            percentageText.gameObject.transform.SetParent(transform);

            if (number > -1) percentageText.transform.localScale = new Vector3(20f, 20f, 20f);
            else percentageText.transform.localScale = new Vector3(7f, 27f, 20f);

            percentageText.rectTransform.sizeDelta = new Vector2(0.1f, 0.1f);

            if (number > -1) percentageText.transform.position = numberText.transform.position;
            else percentageText.transform.position = numberText.transform.position;

            percentageText.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            percentageText.fontSize = 0.2f;
            percentageText.color = Color.cyan;
            percentageText.text = "0%";
        }
    }

    public override void gazeInteractEnd()
    {
        base.gazeInteractEnd();
        gazeDuration = 0;
        if (distance < 0.8)
        {
            if (percentageText.gameObject != null)
            {
                Destroy(percentageText.gameObject);
            }
        }
    }
}

