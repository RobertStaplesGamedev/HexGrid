using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

	public GameObject hexPrefab;

	public GridSettings gridSettings;
 
	// Use this for initialization
	void Start () {
		if (gridSettings.mapSystem == GridSettings.MapSystem.cube) {
			DrawCube();
		}
		else if (gridSettings.mapSystem == GridSettings.MapSystem.offset) {
			DrawOffset();
		}
	}
	void DrawOffset() {
		gridSettings.hexgrid = new Hex[gridSettings.gridWidth,gridSettings.gridHeight];

		for	(int x= 0; x < gridSettings.gridWidth; x++) {
			for (int y = 0; y < gridSettings.gridHeight; y++) {
					
				float xPos = x * gridSettings.XOffset;
				bool xOffsetRow = false;

				GameObject hex_go;
				Hex targetHexScript;

				if (y % 2 == 1 && gridSettings.coordinateOffsetSystem == GridSettings.Offset.odd){
					xPos += gridSettings.XOffset/2f;
					xOffsetRow = true;
				}
				else if (y % 2 == 0 && gridSettings.coordinateOffsetSystem == GridSettings.Offset.even) {
					xPos += gridSettings.XOffset/2f;
					xOffsetRow = true;
				}
				if (gridSettings.startingPoint == GridSettings.StartingPoint.top) {
					hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos ,0, -(y) * gridSettings.ZOffset), Quaternion.identity);
				}
				else {
					hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos ,0, y * gridSettings.ZOffset), Quaternion.identity);
				}
				hex_go.name = "Hex_" + x + "_" + y;
				hex_go.transform.SetParent(this.transform);
				hex_go.isStatic = true;

				targetHexScript = hex_go.transform.GetComponent<Hex>();
				targetHexScript.xPos = x;
				targetHexScript.yPos = y;
				targetHexScript.offsetRow = xOffsetRow;

				gridSettings.hexgrid[x,y] = targetHexScript;
			}
		}
		//Add Hex map
		for	(int x= 0; x < gridSettings.gridWidth; x++) {
			for (int y = 0; y < gridSettings.gridHeight; y++) {
				gridSettings.hexgrid[x,y].SetNeighbours(gridSettings);
			}
		}
	}
	void DrawCube() {
		gridSettings.hexgridCubal = new Hex[gridSettings.radius * 2, gridSettings.radius * 2, gridSettings.radius * 2];

		int rowNum = gridSettings.radius;
		bool ascending = true;

		for (int y = 0; y < (gridSettings.radius * 2 -1); y++) {
			for (int x = 0; x < rowNum; x++) {
				int xPos;
				int yPos;
				int zPos;

				GameObject hex_go;
				Hex targetHexScript;


				if (ascending) {
					xPos = x - (gridSettings.radius-1);
					yPos = rowNum - x - gridSettings.radius;
					zPos = gridSettings.radius - 1 - y;
				}
				else {
					yPos = gridSettings.radius-1 - x;
					xPos = gridSettings.radius + x - rowNum;
					zPos = gridSettings.radius-1 - y;
				}

				float xOffset = xPos * gridSettings.XOffset;
				if (zPos >= 0) {
					xOffset += (gridSettings.XOffset/2) * zPos;
					hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xOffset ,0, -(zPos) * gridSettings.ZOffset), Quaternion.identity);
				}
				else {
					xOffset = yPos * gridSettings.XOffset;
					xOffset += (gridSettings.XOffset/2) * zPos;
					hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(-(xOffset) ,0, -(zPos) * gridSettings.ZOffset), Quaternion.identity);
				}


				hex_go.transform.SetParent(this.transform);
				hex_go.isStatic = true;

				targetHexScript = hex_go.transform.GetComponent<Hex>();

				gridSettings.hexgridCubal[xPos+(gridSettings.radius-1),yPos+(gridSettings.radius-1),zPos+(gridSettings.radius-1)] = targetHexScript;
				hex_go.name = "Hex_" + xPos + "_" + yPos + "_" + zPos;
				targetHexScript.xPos = xPos;
				targetHexScript.yPos = yPos;
				targetHexScript.zPos = zPos;

			}
			if (ascending) {
				if (rowNum == (gridSettings.radius * 2) - 1) {
					ascending = false;
					rowNum--;
				}
				else {
					rowNum++;
				}
			}
			else {
				rowNum--;
			}
		}
		//Add Hex map
		for	(int x= 0; x < gridSettings.radius * 2; x++) {
			for (int y = 0; y < gridSettings.radius * 2; y++) {
				for (int z = 0; z < gridSettings.radius * 2; z++) {
					if (gridSettings.hexgridCubal[x,y,z] != null) {
						gridSettings.hexgridCubal[x,y,z].SetNeighbours(gridSettings);
					}
				}
			}
		}	
	}
}
