using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : GazeInteractable
{
    protected override void Start()
    {
        base.Start();
        activateAction.action.performed += OnActivateAction;
    }

    private void OnDestroy()
    {
        activateAction.action.performed -= OnActivateAction;
    }

    private void OnActivateAction(InputAction.CallbackContext context)
    {
        if (isGazed)
        {
            Debug.Log("restart");
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
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
