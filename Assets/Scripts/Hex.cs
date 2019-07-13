using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

	[HideInInspector]
	public int xPos;
	[HideInInspector]
	public int yPos;
	[HideInInspector]
	public int zPos;
	[HideInInspector]
	public bool offsetRow;
	
	public Hex[] neighbours;
	public Hex[] diagonals;

	public List<Unit> unitsOnHex;

	public enum Terrain {Mountain, grass}
	public Terrain terrain;

	public void SetNeighbours(GridSettings settings) {
		neighbours = new Hex[6];
		diagonals = new Hex[6];
		if (settings.mapSystem == GridSettings.MapSystem.cube) {
			CubeNeighbours(settings);
			CubedDiagonals(settings);
		}
		else if (settings.mapSystem == GridSettings.MapSystem.offset) {
			OffsetNeighbours(settings);
		}
	}
	void CubeNeighbours(GridSettings settings) {
		//Debug.Log(xPos + " " + yPos + " " + zPos);
		if (yPos+ 1 < settings.radius) {
			if (xPos-1 > -(settings.radius)) {
				neighbours[0] = GameObject.Find("Hex_" + (xPos-1) + "_" + (yPos+1) + "_" + zPos).GetComponent<Hex>();
			} 
			if (zPos-1 > -(settings.radius)) {
				
				neighbours[1] = GameObject.Find("Hex_" + xPos + "_" + (yPos+1) + "_" + (zPos-1)).GetComponent<Hex>();
			}
		}
		if (xPos+ 1 < settings.radius) {
			if (zPos-1 > -(settings.radius)) {
				neighbours[2] = GameObject.Find("Hex_" + (xPos+1) + "_" + yPos + "_" + (zPos-1)).GetComponent<Hex>();
			}
			if (yPos-1 > -(settings.radius)) {
				neighbours[3] = GameObject.Find("Hex_" + (xPos+1) + "_" + (yPos-1) + "_" + zPos).GetComponent<Hex>();
			}
		}
		if (zPos+ 1 < settings.radius) {
			if (yPos-1 > -(settings.radius)) {
				neighbours[4] = GameObject.Find("Hex_" + xPos + "_" + (yPos-1) + "_" + (zPos+1)).GetComponent<Hex>();
			} 
			if (xPos-1 > -(settings.radius)) {
				neighbours[5] = GameObject.Find("Hex_" + (xPos-1) + "_" + yPos + "_" + (zPos+1)).GetComponent<Hex>();
			}
		}
	}
	void CubedDiagonals(GridSettings settings) {
		if (zPos+ 1 < settings.radius && yPos+ 1 < settings.radius && xPos-2 > -(settings.radius)) {	
			diagonals[0] = GameObject.Find("Hex_" + (xPos-2) + "_" + (yPos+1) + "_" + (zPos+1)).GetComponent<Hex>();
		}	if (zPos-1 > -(settings.radius) && yPos+ 2 < settings.radius && xPos-1 > -(settings.radius)) {
			diagonals[1] = GameObject.Find("Hex_" + (xPos-1) + "_" + (yPos+2) + "_" + (zPos-1)).GetComponent<Hex>();
		}	if (xPos+ 1 < settings.radius && yPos+ 1 < settings.radius && zPos-2 > -(settings.radius)) {
			diagonals[2] = GameObject.Find("Hex_" + (xPos+1) + "_" + (yPos+1) + "_" + (zPos-2)).GetComponent<Hex>();
		}	if (xPos+ 2 < settings.radius && yPos-1 > -(settings.radius) && zPos-1 > -(settings.radius)) {
			diagonals[3] = GameObject.Find("Hex_" + (xPos+2) + "_" + (yPos-1) + "_" + (zPos-1)).GetComponent<Hex>();
		}	if (xPos+ 1 < settings.radius && yPos-2 > -(settings.radius) && zPos+ 1 < settings.radius) {
			diagonals[4] = GameObject.Find("Hex_" + (xPos+1) + "_" + (yPos-2) + "_" + (zPos+1)).GetComponent<Hex>();
		}	if (xPos-1 > -(settings.radius) && yPos-1 > -(settings.radius) && zPos+2 < settings.radius) {
			diagonals[5] = GameObject.Find("Hex_" + (xPos-1) + "_" + (yPos-1) + "_" + (zPos+2)).GetComponent<Hex>();
		}	
	}
	void OffsetNeighbours(GridSettings settings) {
		if (xPos-1 >=0) {
			neighbours[0] = GameObject.Find("Hex_" + (xPos-1) + "_" + yPos).GetComponent<Hex>();
		}
		if (xPos+1 <= settings.gridWidth-1) {
			neighbours[3] = GameObject.Find("Hex_" + (xPos + 1) + "_" + yPos).GetComponent<Hex>();
		}
		if (offsetRow) {
			if (yPos+1 <= settings.gridHeight-1) {
				neighbours[1] = GameObject.Find("Hex_" + xPos + "_" + (yPos+1)).GetComponent<Hex>();
				if (xPos+1 <= settings.gridWidth-1) {
					neighbours[2] = GameObject.Find("Hex_" + (xPos+1) + "_" + (yPos+1)).GetComponent<Hex>();
				}
			}
			if (yPos-1 >= 0) {
				neighbours[5] = GameObject.Find("Hex_" + xPos + "_" + (yPos-1)).GetComponent<Hex>();
				if (xPos+1 <= settings.gridWidth-1) {
					neighbours[4] = GameObject.Find("Hex_" + (xPos+1) + "_" + (yPos-1)).GetComponent<Hex>();
				}
			}		
		} else {
			if (yPos+1 <= settings.gridHeight-1) {
				if (xPos-1 >=0) {
					neighbours[1] = GameObject.Find("Hex_" + (xPos-1) + "_" + (yPos+1)).GetComponent<Hex>();
				}
				neighbours[2] = GameObject.Find("Hex_" + xPos + "_" + (yPos+1)).GetComponent<Hex>();
			}
			if (yPos-1 >= 0) {
				neighbours[4] = GameObject.Find("Hex_" + xPos + "_" + (yPos-1)).GetComponent<Hex>();
				if (xPos-1 >=0) {
					neighbours[5] = GameObject.Find("Hex_" + (xPos-1) + "_" + (yPos-1)).GetComponent<Hex>();
				}
			}	
		}
	}
}
