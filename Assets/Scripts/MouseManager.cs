using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

	Unit selectedUnit;
	public Hex originHex;
	MeshRenderer hexMesh;
	Hex hoverHex;
	MeshRenderer hoverHexMesh;
	List<MeshRenderer> meshLine;

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
			
			if (hitInfo.collider.gameObject.GetComponent<Unit>() != null) {
				GameObject ourHitObject = hitInfo.collider.gameObject;
				OnUnitClick(ourHitObject);
			}
			else if(hitInfo.collider.transform.parent.gameObject.GetComponent<Hex>() != null) {
				GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;
				OnHover(ourHitObject);
				OnHexClick(ourHitObject);
			}
		}
	}
	void OnHover(GameObject ourHitObject) {
		if (originHex != null)
			DrawLine(ourHitObject);
		else {
			//ColorHex(ourHitObject);
		}
	}
	void OnHexClick(GameObject ourHitObject) {
		// if (Input.GetMouseButtonDown(0)) {
				
		// 	//ColorHex(ourHitObject);
		// 	SetLineOrigin(ourHitObject);

		// }
		if (Input.GetMouseButtonDown(1)) {
			if (selectedUnit != null)
			{
				selectedUnit.MoveToHex(ourHitObject.GetComponent<Hex>());
				SetLineOrigin(selectedUnit.currentHex.gameObject);
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
		if (meshLine == null) {
			meshLine = new List<MeshRenderer>();
		}
		if (hoverHex == null) {
			hoverHexMesh = ourHitObject.GetComponentInChildren<MeshRenderer>();
			hoverHexMesh.material.color = Color.blue;
			hoverHex = ourHitObject.GetComponentInChildren<Hex>();
		} else if (ourHitObject != originHex){
			//Uncolour non selection
			hoverHexMesh.material.color = Color.white;
			//Uncolour old lines
			foreach (MeshRenderer hex in meshLine) {
				hex.material.color = Color.white;
			}
			//Colour lines
			List<int[]> line = CoordinateHelpers.CalculateLine(originHex, hoverHex);
			foreach (int[] coordTri in line) {
				try {
					MeshRenderer hexInLine = GameObject.Find("Hex_" + coordTri[0] + "_" + coordTri[1] + "_" + coordTri[2]).GetComponentInChildren<MeshRenderer>();
					if (hexInLine != hoverHex && hexInLine != hexMesh) {
						hexInLine.material.color = new Color(.35f, .44f, 1f, 1);
						meshLine.Add(hexInLine);
					}
				}
				catch {
					Debug.Log("Error: " + coordTri[0] + "_" + coordTri[1] + "_" + coordTri[2]);
				}
			}
			hoverHexMesh = ourHitObject.GetComponentInChildren<MeshRenderer>();
			hoverHexMesh.material.color = Color.blue;
			hexMesh.material.color = Color.blue;
			hoverHex = ourHitObject.GetComponentInChildren<Hex>();
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
		if (Input.GetMouseButtonDown(0)) {
			if (selectedUnit != null ) {
				selectedUnit.UnSelectUnit();
			}
			selectedUnit = ourHitObject.GetComponent<Unit>();
			selectedUnit.SelectUnit();
			SetLineOrigin(selectedUnit.currentHex.gameObject);
			DrawLine(selectedUnit.currentHex.gameObject);
		}
	}
}
