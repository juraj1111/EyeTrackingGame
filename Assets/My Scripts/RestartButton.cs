using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : GazeInteractable
{
    private void Update()
    {
        if(isGazed)
        {
            if (activateAction.action.triggered)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
            }
        }
    }

    public override void onFirstLook(float distance)
    {
        GetComponent<Image>().color = Color.yellow;
    }
    public override void gazeInteractEnd()
    {
        GetComponent<Image>().color = Color.white;
    }
}
