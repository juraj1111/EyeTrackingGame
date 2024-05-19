using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(InventoryManager))]
public class DebugButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InventoryManager script = (InventoryManager)target;

        if (GUILayout.Button("Show Inventory"))
        {
            script.ShowInventory();
        }
    }
}
