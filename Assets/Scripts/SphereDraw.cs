using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDraw : MonoBehaviour {

	public GameObject hexPrefab;
	public int radius = 6;
	public SphereSettings sphereSettings;

	GameObject[] hexGrid;

	// Use this for initialization
	void Start () {

		hexGrid = new GameObject[radius * 2];

		int r = 0;
		for (int i = 0; i < radius * 2; i++, r= r + sphereSettings.rotation) {
			GameObject hex_go;

			if (i == 0) {
				hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(0,0,0), Quaternion.identity);
			}
			else {
				hex_go = (GameObject)Instantiate(hexPrefab, Hexpos(r, hexGrid[i-1].transform.position.x, hexGrid[i-1].transform.position.z), Quaternion.identity);
			}
			hex_go.name = "Hex_0_" + i;
			hex_go.transform.Rotate(0,r,0);
			hex_go.transform.SetParent(this.transform);
			hex_go.isStatic = true;
			hexGrid[i] = hex_go;

			hex_go.GetComponentInChildren<MeshRenderer>().material.color = Random.ColorHSV();
		}
	}
	Vector3 Hexpos(int rotation, float x, float y)
	{
		float hyp = sphereSettings.hypotinuse;
		x = x + (hyp * Mathf.Cos((rotation - ((float)(sphereSettings.rotation) /2)) * Mathf.Deg2Rad));

		y = -(y);
		y = y + (hyp * Mathf.Sin((rotation - ((float)(sphereSettings.rotation) /2)) * Mathf.Deg2Rad));
		//Debug.Log(y);

		return new Vector3(x,0,-(y));
	}
	
}
