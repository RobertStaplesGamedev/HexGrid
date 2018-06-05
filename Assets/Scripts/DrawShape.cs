using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class DrawShape : MonoBehaviour {
    
    Vector3[] normals;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] tris;

    public enum ShapeDraw {Pentagon, Hexagon}
    public ShapeDraw shape;

    Vector3 center = new Vector3(0,0,0);

    float size = 0.5f;
    public bool debug;

    void Start() {
        
        if (shape == ShapeDraw.Pentagon) {
            vertices =new Vector3[5];
        }
        else {
            vertices =new Vector3[6];
        }
        uvs =new Vector2[vertices.Length];
        normals = new Vector3[vertices.Length];
        if (shape == ShapeDraw.Pentagon) {
            tris = new int[9];
        }
        else {
            tris = new int[12];
        }

        //Vertices
        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = DrawVertex(i);
        }

        int triCount = 1;
        for (int i = 0; i < tris.Length; i += 3) {
            tris[i] = triCount;
            tris[i+1] = 0;
            tris[i+2] = triCount + 1;
            triCount++;
        }

        for (int i = 0; i < normals.Length; i++) {
            normals[i] = Vector3.up;
        }

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x - size, vertices[i].z + size);
        }

        Mesh mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.normals = normals;
        mesh.uv = uvs;

        if (debug) {
            for (int i = 0; i < vertices.Length; i++) {
                Debug.Log("Vertex " + (i + 1) + ": " + vertices[i]);
            }
            

            for (int i = 0; i < uvs.Length; i++) {
                Debug.Log("UV " + (i + 1) + ": " + uvs[i]);
            }
        }
    }
    Vector3 DrawVertex(int angle) {
        float angle_deg;
        if (shape == ShapeDraw.Pentagon) {
            angle_deg = 72 * angle - 90;
        }
        else {
            angle_deg = 60 * angle - 90;
        }
        float angle_rad = (float)(Math.PI) / 180 * angle_deg;
        //Debug.Log((float)(center.x + size * Math.Cos(angle_rad)));
        return new Vector3((float)(center.x + size * Math.Cos(angle_rad)), 0, (float)(center.y +size * Math.Sin(angle_rad)));
    }
}