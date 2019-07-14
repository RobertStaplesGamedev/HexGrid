using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int playersTurn = 1;
    public TMP_Text playerTurnText;

    public LocalPlayer player1;
    public LocalPlayer player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncrementTurn() {
        if (playersTurn == 1) {
            playersTurn = 2;
            playerTurnText.text = "Player 2's turn";
            player2.ResetUnits();
        } else {
            playersTurn = 1;
            playerTurnText.text = "Player 1's turn";
            player1.ResetUnits();
        }
        foreach (Unit unit in player1.unitsAlive) {
            unit.currentHex.CheckForMeleeAttacks();
        }
    }

    public void OnClickEndTurn() {
        IncrementTurn();
    }

    public void CheckWinner() {
        if (player1.unitsAlive.Count == 0) {
            //Player 2 wins
        } else if (player2.unitsAlive.Count == 0) {
            //player1 wins
        }
    }
}
