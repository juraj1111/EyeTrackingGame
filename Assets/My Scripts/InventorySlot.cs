using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{

    /*private GameObject item;
    public float scaleFactor = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item") && item == null)
        {
            item = other.gameObject;
            Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = true;
            }
            item.transform.SetParent(transform, false); // Make the transform changes local
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            /*Vector3 slotSize = transform.localScale;
            Vector3 itemSize = item.transform.localScale;
            Vector3 desiredScale = new Vector3(
                slotSize.x / itemSize.x * scaleFactor,
                slotSize.y / itemSize.y * scaleFactor,
                slotSize.z / itemSize.z * scaleFactor
            );*//*

            item.transform.localScale = Vector3.one * 10; // Reset local scale
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (item != null && other.gameObject == item)
        {
            item.transform.SetParent(null); // Unparent the item
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.localScale = Vector3.one;
            item = null;

            XRSocketInteractor socketInteractor;
            socketInteractor.GetOldestInteractableSelected
        }
    }*/

}
