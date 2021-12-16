using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Cable : MonoBehaviour
{

	[SerializeField]
	public List<CableCurve> cables;
	public Material cableMatr;
	public Space space;

	bool firstRun = true;

	public void Start()
	{
		GenerateMesh();
	}

	public void GenerateMesh()
	{
		Transform tr = transform;
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

		Mesh cableMesh = meshFilter.sharedMesh;
		if (cableMesh == null)
			cableMesh = new Mesh();
		cableMesh.Clear();

		int numCables = (cables == null) ? 0 : cables.Count;
		cableMesh.subMeshCount = numCables;

		if (firstRun)
		{
			for (int c = 0; c < numCables; c++)
			{
				cables[c].regenPoints = true;
			}
			firstRun = false;
		}

		List<Vector3> points = new List<Vector3>();
		List<Color> colors = new List<Color>();
		for (int c = 0; c < numCables; c++)
		{
			points.AddRange(cables[c].Points());
			colors.AddRange(cables[c].Colors());
		}

		if (space == Space.World)
		{
			int count = points.Count;
			for (int p = 0; p < count; p++)
			{
				Matrix4x4 ltw = tr.worldToLocalMatrix;
				points[p] = ltw.MultiplyPoint(points[p]);
			}
		}

		cableMesh.SetVertices(points);
		cableMesh.SetColors(colors);

		int indice = 0;
		for (int c = 0; c < numCables; c++)
		{
			int numIndices = cables[c].steps;
			int[] indices = new int[numIndices];
			for (int i = 0; i < numIndices; i++)
			{
				indices[i] = indice;
				indice++;
			}
			cableMesh.SetIndices(indices, MeshTopology.LineStrip, c);
		}

		cableMesh.RecalculateBounds();
		meshFilter.sharedMesh = cableMesh;
		AssignMaterials();
	}

	public void AssignMaterials()
	{
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		int numCables = (cables == null) ? 0 : cables.Count;
		Material[] mats = new Material[numCables];
		for (int c = 0; c < numCables; c++)
		{
			mats[c] = cableMatr;
		}
		meshRenderer.sharedMaterials = mats;
	}


}