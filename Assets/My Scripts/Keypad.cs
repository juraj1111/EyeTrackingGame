using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    [SerializeField] public int correctPassword = 1234;
    private int givenPassword;
    private int currentDigit;
    private int lastNumber;
    TMP_FontAsset originalFont;

    public TextMeshPro textMeshProObject;
    public GameObject doorParent;
    public LockedDoors lockedDoors;

    private void Start()
    {
        givenPassword = 0;
        currentDigit = 3;
        originalFont = textMeshProObject.font;
    }

    public bool add(int number)
    {
        //Debug.Log("added " + number);
        givenPassword = givenPassword + number * (int)Mathf.Pow(10, currentDigit);
        //Debug.Log("given password " + givenPassword);
        StartCoroutine(updateDisplayNumber(0));
        currentDigit--;
        lastNumber = number;
        if (currentDigit < 0)
        {
            if (correctPassword == givenPassword)
            {
                StartCoroutine(updateDisplayNumber(1));
                lockedDoors.unlock();
                return true;
            }
            else
            {
                StartCoroutine(updateDisplayNumber(2));         
                currentDigit = 3;
                return false;
            }
        }
        return false;
    }

    public void remove()
    {
        if (currentDigit == 3) return;
        givenPassword = givenPassword - lastNumber * (int)Mathf.Pow(10, currentDigit + 1);
        StartCoroutine(updateDisplayNumber(0));
        currentDigit++;
    }

    IEnumerator updateDisplayNumber(int state)
    {
        if(textMeshProObject != null)
        {
            if (state == 0)
            {
                if(givenPassword == 0) textMeshProObject.text = "0000";
                else textMeshProObject.text = givenPassword.ToString();
            }
            else if(state == 1)
            {
                textMeshProObject.text = givenPassword.ToString();
                textMeshProObject.color = Color.green;
            }
            else if(state == 2)
            {
                textMeshProObject.text = givenPassword.ToString();
                textMeshProObject.color = Color.red;
                yield return new WaitForSeconds(1f);
                textMeshProObject.color = Color.white;
                givenPassword = 0;
                StartCoroutine(updateDisplayNumber(0));
            }
            textMeshProObject.font = originalFont;
        }
    }
}
