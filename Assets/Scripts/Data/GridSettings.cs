using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class GridSettings : ScriptableObject{
	
	public enum Offset {odd, even}
	public enum StartingPoint {top, bottom}
	public enum MapSystem {offset, cube}

	public MapSystem mapSystem;

	[HideInInspector]
	public Hex[,] hexgrid;

	public float XOffset = 0.882f;
	public float ZOffset = 0.75f;

	[Header("Cubal")]
	public int radius = 4;
	[HideInInspector]
	public Hex[,,] hexgridCubal;

	[Header("Offet")]
	//Offset System to generate hexgrid
	public GridSettings.Offset coordinateOffsetSystem;
	//Do the coordinates start at the top left or bottom left
	public GridSettings.StartingPoint startingPoint;
	//Width of map in Tiles
	public int gridWidth = 20;
	//Height if map in Tiles
	public int gridHeight = 20;

}
