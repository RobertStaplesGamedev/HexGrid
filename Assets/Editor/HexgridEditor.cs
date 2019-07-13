using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridController))]
public class HexgridEditor : Editor
{
    bool isDrawn = false;


    public override void OnInspectorGUI() {
        GridController gridController = (GridController)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Draw")) {
            if (isDrawn) {
                Debug.Log("Already Drawn");
            } else {
                gridController.DrawCube();
                isDrawn = true;
            }

        }
        if (GUILayout.Button("Reset")) {
            if (isDrawn) {
                while(gridController.transform.childCount != 0){
                    DestroyImmediate(gridController.transform.GetChild(0).gameObject);
                }
                isDrawn = false;
            }
            else {
                Debug.Log("Grid not drawn");
            }
        }
    }
}
