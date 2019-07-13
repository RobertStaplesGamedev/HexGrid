using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public Hex currentHex;
	Vector3 destination;

	public Color unitColor;
	public Color unitSelectedColor;

	bool atLocation = true;
	bool isSelected = false;

	float destinationRange = 0.05f;

	float speed = 4;

	//Movement Variables
	public bool isFlying;
	public int movementRange;

	//Attack Variables
	public int health;
	public bool isMelee;
	public int attackStrength;
	public int attackRange;

	public void UnitCreation(Hex hex, Color playerColor, Color playerSelectedColor) {
		currentHex = hex;
		currentHex.unitsOnHex = new List<Unit>();
		currentHex.unitsOnHex.Add(this);
        unitColor = playerColor;
        unitSelectedColor = playerSelectedColor;
	}

	public void SelectUnit() {
		//Move the current
		isSelected = true;
		this.GetComponent<MeshRenderer>().material.color = unitSelectedColor;
	}

	public void UnSelectUnit() {
		isSelected = false;
		this.GetComponent<MeshRenderer>().material.color = unitColor;
	}

	public void MoveToHex(Hex destinationHex) {
		// This is where all my code for unity movement is.
		if (atLocation) {
			atLocation = false;

			destination = destinationHex.GetComponent<Transform>().position;

			currentHex.unitsOnHex.Remove(this);
			currentHex = destinationHex;
		}
	}

	void Update() {
		if (!atLocation) {
			Vector3 dir = destination - transform.position;
			Vector3 velocity = dir.normalized * speed * Time.deltaTime;

			velocity = Vector3.ClampMagnitude(velocity, dir.magnitude);

			transform.Translate(velocity);

			if (destination != null && currentHex != null) {
				atLocation = CheckifInRange();
			}
		}
	}

	bool CheckifInRange() {
		bool isAtLocation = false;
		bool isatX = false;
		
		bool isatY = false;

		if (Mathf.Abs(transform.position.x - currentHex.GetComponent<Transform>().position.x) <= destinationRange) {
			isatX = true;
		}
		if (Mathf.Abs(transform.position.z - currentHex.GetComponent<Transform>().position.z) <= destinationRange) {
			isatY = true;
		}


		if (isatX && isatY) {
			isAtLocation = true;
			
			if (currentHex.unitsOnHex == null) {
				currentHex.unitsOnHex = new List<Unit>();
			}
			currentHex.unitsOnHex.Add(this);
		}
		return isAtLocation;
	}
}
