using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject map;

    public GameObject unitPrefab;

    public List<Unit> unitsAlive;

    public Color playerColor;
    public Color playerSelectedColor;

    public void CreateUnits() {
        if (playerNumber == 1) {
            CreateUnit(map.transform.Find("Hex_-5_0_5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_-4_-1_5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_-3_-2_5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_-2_-3_5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_-1_-4_5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_0_-5_5").GetComponent<Hex>());
        } else {
            CreateUnit(map.transform.Find("Hex_0_5_-5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_1_4_-5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_2_3_-5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_3_2_-5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_4_1_-5").GetComponent<Hex>());
            CreateUnit(map.transform.Find("Hex_5_0_-5").GetComponent<Hex>());
        }
    }

    void CreateUnit(Hex hex) {
        GameObject newUnit = Instantiate(unitPrefab, hex.transform.position, Quaternion.identity);
        newUnit.GetComponent<MeshRenderer>().material.color = playerColor;
        unitsAlive.Add(newUnit.GetComponent<Unit>());
        newUnit.transform.SetParent(this.transform);

        //TODO: Need to set all unit variables
        newUnit.GetComponent<Unit>().movementRange = 5;
        newUnit.GetComponent<Unit>().attackStrength = 1;
        newUnit.GetComponent<Unit>().health = 5;
        newUnit.GetComponent<Unit>().UnitCreation(hex, playerColor, playerSelectedColor);
    }
}
