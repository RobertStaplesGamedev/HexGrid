using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

	Unit selectedUnit;
	Hex originHex;
	MeshRenderer hexMesh;
	Hex hoverHex;
	MeshRenderer hoverHexMesh;

	// Update is called once per frame
	void Update () {
		
		if (EventSystem.current)
		{
			if (EventSystem.current.IsPointerOverGameObject()) {
				return;
			}
		}

		//Vector3 worldpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo)) 
		{
			GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;
			
			if(ourHitObject.GetComponent<Hex>() != null) {
				OnHover(ourHitObject);
				OnHexClick(ourHitObject);
			}
			else if (ourHitObject.GetComponent<Unit>() != null) {
				//OnUnitClick(ourHitObject);
			}
		}
	}
	void OnHover(GameObject ourHitObject) {
		if (originHex != null)
			DrawLine(ourHitObject);
	}
	void OnHexClick(GameObject ourHitObject) {
		if (Input.GetMouseButtonDown(0)) {
				
			//ColorHex(ourHitObject);
			SetLineOrigin(ourHitObject);

			if (selectedUnit != null)
			{
				selectedUnit.destination = ourHitObject.transform.position;
			}
		}
	}
	void SetLineOrigin(GameObject ourHitObject) {
		if (originHex == null) {
			hexMesh = ourHitObject.GetComponentInChildren<MeshRenderer>();
			hexMesh.material.color = Color.blue;
			originHex = ourHitObject.GetComponentInChildren<Hex>();
		} else {
			hexMesh.material.color = Color.white;
			hexMesh = ourHitObject.GetComponentInChildren<MeshRenderer>();
			hexMesh.material.color = Color.blue;
			originHex = ourHitObject.GetComponentInChildren<Hex>();
		}
	}
	void DrawLine(GameObject ourHitObject) {
		if (hoverHex == null) {
			hoverHexMesh = ourHitObject.GetComponentInChildren<MeshRenderer>();
			hoverHexMesh.material.color = Color.blue;
			hoverHex = ourHitObject.GetComponentInChildren<Hex>();
		} else if (ourHitObject.GetComponentInChildren<MeshRenderer>() != hexMesh){
			hoverHexMesh.material.color = Color.white;
			hoverHexMesh = ourHitObject.GetComponentInChildren<MeshRenderer>();
			hoverHexMesh.material.color = Color.blue;
			hoverHex = ourHitObject.GetComponentInChildren<Hex>();
			CoordinateHelpers.CalculateLine(originHex, hoverHex);
		}
	}
	void ColorHex(GameObject ourHitObject) {
		MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

		if (mr.material.color == Color.red)
			mr.material.color = Color.white;
		else
			mr.material.color = Color.red;
		Hex[] neighbours = ourHitObject.GetComponent<Hex>().neighbours;
		Hex[] diagonals = ourHitObject.GetComponent<Hex>().diagonals;
		for (int i=0; i < neighbours.Length; i++) {
			if (neighbours[i] != null) {
				MeshRenderer mr2 = neighbours[i].GetComponentInChildren<MeshRenderer>();
				if (mr.material.color != Color.red) 
					mr.material.color = Color.blue;
			}
		}
		for (int i=0; i < diagonals.Length; i++) {
			if (diagonals[i] != null) {
				MeshRenderer mr2 = diagonals[i].GetComponentInChildren<MeshRenderer>();
				if (mr.material.color != Color.red) 
					mr.material.color = Color.green;
			}
		}
	}
	void OnUnitClick(GameObject ourHitObject) {
		selectedUnit = ourHitObject.GetComponent<Unit>();
	}
}
