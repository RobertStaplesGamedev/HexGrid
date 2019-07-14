using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour {

	public Hex currentHex;
	Vector3 destination;

	bool atLocation = true;
	bool isSelected = false;

	float destinationRange = 0.35f;

	float speed = 4;

	//Player Variables
	public Color unitColor;
	public Color unitSelectedColor;
	public LocalPlayer playerOwned;

	//Movement Variables
	public bool isFlying;
	public int movementRange;
	public int movementThisTurn;

	//Attack Variables
	public int health;
	public bool isMelee;
	public int attackStrength;
	public int attackRange;

	public void UnitCreation(Hex hex, LocalPlayer player, Color playerColor, Color playerSelectedColor) {
		playerOwned = player;
		currentHex = hex;
		currentHex.unitsOnHex = new List<Unit>();
		currentHex.unitsOnHex.Add(this);
        unitColor = playerColor;
        unitSelectedColor = playerSelectedColor;
		movementThisTurn = movementRange;
	}

	public void SelectUnit() {
		//Move the current
		isSelected = true;
		this.GetComponent<MeshRenderer>().material.color = unitSelectedColor;
		this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		//this.transform.GetChild(0).GetChild(0).GetChild(0).position = calculateWorldPosition(this.transform.GetChild(0).position, Camera.main);
	}

	public void UnSelectUnit() {
		isSelected = false;
		this.GetComponent<MeshRenderer>().material.color = unitColor;
		this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
	}

	public void MoveToHex(Hex destinationHex, int distance) {
		// This is where all my code for unity movement is.

		movementThisTurn -= distance;
		if (atLocation) {
			atLocation = false;

			destination = destinationHex.GetComponent<Transform>().position;

			currentHex.unitsOnHex.Remove(this);
			currentHex = destinationHex;
		}
	}

	void Update() {
		DrawStats();
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

	public void ResetMovement() {
		movementThisTurn = movementRange;
	}

	public void DrawStats() {
		this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = string.Format("Health {0}, Movement {1}, Attack {2}", health, movementThisTurn, attackStrength);
	}
	public void DestroyUnit() {
		playerOwned.unitsAlive.Remove(this);
		Destroy(this.gameObject);
	}
}
