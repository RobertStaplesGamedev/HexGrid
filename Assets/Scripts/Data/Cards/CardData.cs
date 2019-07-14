using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CardData : ScriptableObject
{
    public string title;
    public string Descripton;

    public int cyberneticCost;
    public int terraformicCost;
    public int adaptationCost;
    public int corruptionCost;

    public enum CardType {HeathModifier, MovementModifier, AttackModifier, UnitCreation, Damage, Building, TileModifier}
    public CardType cardType;

    public Unit unit;
    public int value;

    public bool isPermanant;
}
