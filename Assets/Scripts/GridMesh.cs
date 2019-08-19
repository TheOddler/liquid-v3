using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class GridMesh : MonoBehaviour
{
	public int _subdivisions = 128;
	public float _size = 10;

	public void Regen()
	{
		var mesh = GetComponent<MeshFilter>().mesh = new Mesh();
		mesh.name = "Grid " + _subdivisions;

		var vertices = new Vector3[(_subdivisions+1) * (_subdivisions+1)];
		var uvs = new Vector2[(_subdivisions+1) * (_subdivisions+1)];
		for (int i = 0, y = 0; y <= _subdivisions; ++y)
		{
			for (int x = 0; x <= _subdivisions; ++x, ++i)
			{
				vertices[i] = new Vector3(
					-_size/2f + (float)x/_subdivisions*_size, 
					0, 
					-_size/2f + (float)y/_subdivisions*_size
				);
				uvs[i] = new Vector2((float)x / _subdivisions, (float)y / _subdivisions);
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uvs;

		int[] triangles = new int[_subdivisions * _subdivisions * 6];
		for (int ti = 0, vi = 0, y = 0; y < _subdivisions; y++, vi++) {
			for (int x = 0; x < _subdivisions; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 1] = vi + _subdivisions + 1;
				triangles[ti + 2] = vi + 1;
				triangles[ti + 3] = vi + 1;
				triangles[ti + 4] = vi + _subdivisions + 1;
				triangles[ti + 5] = vi + _subdivisions + 2;
				/*triangles[ti] = vi;
				triangles[ti + 1] = vi + _count + 1;
				triangles[ti + 2] = vi + _count + 2;
				triangles[ti + 3] = vi;
				triangles[ti + 4] = vi + _count + 2;
				triangles[ti + 5] = vi + 1;*/
			}
		}
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
	}

	public void Print()
	{
		var mesh = GetComponent<MeshFilter>().sharedMesh;
		Debug.Log("verts: " + string.Join("; ", mesh.vertices.Select(v => "(" + v.x + "," + v.y + "," + v.z + ")").ToArray()));
		Debug.Log("uvs: " + string.Join("; ", mesh.uv.Select(uv => "(" + uv.x + "," + uv.y + ")").ToArray()));
		Debug.Log("tris: " + string.Join("; ", mesh.triangles.Select(t => t.ToString()).ToArray()));
	}
}



#if UNITY_EDITOR
[CustomEditor(typeof(GridMesh))]
public class GridMeshEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        GridMesh myTarget = (GridMesh)target;

		DrawDefaultInspector();
        
		if (GUILayout.Button("Regenerate"))
		{
			myTarget.Regen();
		}
        
		if (GUILayout.Button("Print"))
		{
			myTarget.Print();
		}
    }
}
#endif