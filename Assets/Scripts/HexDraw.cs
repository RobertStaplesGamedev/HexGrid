using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class HexDraw : MonoBehaviour {
    
    Vector3[] normals;
    Vector3[] verticies;
    Vector2[] uvs;
    int[] tris;

    public int size = 4;
    public int radius = 4;

    void Start() {
        Vector3 center = new Vector3(0,0,0);
        verticies =new Vector3[24];
        uvs =new Vector2[24];

        //DrawHex
        for (int i = 0; i < 6; i++) {
            float angle_deg = 60 * i - 30;
            float angle_rad = (float)(Math.PI) / 180 * angle_deg;
            verticies[i] = new Vector3((float)(size * Math.Cos(angle_rad)), 0, (float)(size * Math.Sin(angle_rad)));
        }
        

        var mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = verticies;
        mesh.uv = uvs;
        mesh.triangles = tris;
        mesh.normals = normals;
    }
}