using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public Camera playerCamera;
    public InputActionReference inventoryAction; //controller menu action
    public XRSocketInteractor[] socketInteractors;
    private GameObject[] gameObjects;


    private void Start()
    {
        inventory.SetActive(false);
        inventoryAction.action.started += ShowInventory;
        gameObjects = new GameObject[4];
    }

    private void ShowInventory(InputAction.CallbackContext obj)
    {
        if(!inventory.activeSelf) {
            inventory.SetActive(true);
            Vector3 cameraPosition = playerCamera.transform.position;
            Vector3 cameraForward = playerCamera.transform.forward;

            Vector3 newPosition = cameraPosition + cameraForward * 0.5f;

            inventory.transform.position = newPosition;
            inventory.transform.rotation = playerCamera.transform.rotation;

            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null)
                {
                    gameObjects[i].SetActive(true);
                    gameObjects[i].transform.position = socketInteractors[i].transform.position;
                }
            }
        }
        else
        {
            for (int i = 0; i < socketInteractors.Length; i++)
            {
                if (socketInteractors[i].hasSelection)
                {
                    gameObjects[i] = socketInteractors[i].GetOldestInteractableSelected().transform.gameObject;
                    socketInteractors[i].GetOldestInteractableSelected().transform.gameObject.SetActive(false);
                }
                else gameObjects[i] = null;
            }
            inventory.SetActive(false);
        }
    }

    public void ShowInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);

            inventory.transform.position = inventory.transform.position + new Vector3(0, 0, 1.0f);
            inventory.transform.rotation = playerCamera.transform.rotation;

            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null)
                {
                    gameObjects[i].SetActive(true);
                    gameObjects[i].transform.position = socketInteractors[i].transform.position;
                }
            }
        }
        else
        {
            for (int i = 0; i < socketInteractors.Length; i++)
            {
                if (socketInteractors[i].hasSelection)
                {
                    gameObjects[i] = socketInteractors[i].GetOldestInteractableSelected().transform.gameObject;
                    socketInteractors[i].GetOldestInteractableSelected().transform.gameObject.SetActive(false);    
                }
                else gameObjects[i] = null;
            }
            inventory.SetActive(false);
        }
    }

}
