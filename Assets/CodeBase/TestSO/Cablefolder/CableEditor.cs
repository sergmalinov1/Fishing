// Place this into directory called Editor in your Assets directory.

using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Cable))]
public class CableEditor : Editor
{

	int highlight = -1;

	void OnSceneGUI()
	{
		Cable cable = (Cable)target;

		Transform tr = cable.transform;
		Quaternion rot = Quaternion.identity;
		Matrix4x4 toWorld = Matrix4x4.identity;
		Matrix4x4 toLocal = Matrix4x4.identity;

		if (cable.space == Space.Self)
		{
			toWorld = tr.localToWorldMatrix;
			toLocal = tr.worldToLocalMatrix;
			rot = tr.rotation;
		}

		int count = (cable.cables == null) ? 0 : cable.cables.Count;

		if (count > 0)
		{
			bool setDirty = false;
			for (int i = 0; i < count; i++)
			{
				CableCurve curve = cable.cables[i];

				// Draw cable index.
				Handles.Label(toWorld.MultiplyPoint(curve.midPoint), i.ToString());

				// Draw highlighted cable.
				if (i == highlight)
				{
					Handles.color = Color.white;
					if (cable.space == Space.Self)
					{
						Vector3[] points = new Vector3[curve.steps];
						Vector3[] curvePoints = curve.Points();
						for (int p = 0; p < points.Length; p++)
						{
							points[p] = toWorld.MultiplyPoint(curvePoints[p]);
						}
						Handles.DrawPolyLine(points);
					}
					else
					{
						Handles.DrawPolyLine(cable.cables[i].Points());
					}
				}

				if (curve.drawHandles)
				{
					EditorGUI.BeginChangeCheck();
					Vector3 start = toLocal.MultiplyPoint(Handles.PositionHandle(toWorld.MultiplyPoint(curve.start), rot));
					Vector3 end = toLocal.MultiplyPoint(Handles.PositionHandle(toWorld.MultiplyPoint(curve.end), rot));

					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(cable, "Edited Cable");
						curve.start = start;
						curve.end = end;
						setDirty = true;
					}
				}
			}

			if (tr.hasChanged && cable.space == Space.World)
			{
				setDirty = true;
				tr.hasChanged = false;
			}

			if (setDirty)
			{
				cable.GenerateMesh();
				EditorUtility.SetDirty(cable);
			}
		}
	}

	public override void OnInspectorGUI()
	{
		Cable cable = (Cable)target;

		int count = (cable.cables == null) ? 0 : cable.cables.Count;

		bool setDirty = false;

		EditorGUI.BeginChangeCheck();
		Space space = (Space)EditorGUILayout.Popup("Space", (int)cable.space, Enum.GetNames(typeof(Space)));
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(cable, "Edited Cable Space");
			cable.space = space;
			setDirty = true;
		}

		EditorGUI.BeginChangeCheck();
		Material cableMatr = (Material)EditorGUILayout.ObjectField("Material", cable.cableMatr, typeof(Material), true);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(cable, "Edited Cable Material");
			cable.cableMatr = cableMatr;
			cable.AssignMaterials();
			setDirty = true;
		}

		EditorGUILayout.Separator();

		highlight = -1;

		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{

				Rect r = EditorGUILayout.BeginVertical();

				CableCurve curve = cable.cables[i];

				EditorGUI.BeginChangeCheck();
				curve.drawHandles = EditorGUILayout.Foldout(curve.drawHandles, i.ToString());
				if (EditorGUI.EndChangeCheck())
				{
					setDirty = true;
				}

				if (curve.drawHandles)
				{
					EditorGUI.BeginChangeCheck();
					Vector3 start = EditorGUILayout.Vector3Field("Start", curve.start);
					Vector3 end = EditorGUILayout.Vector3Field("End", curve.end);
					float slack = EditorGUILayout.FloatField("Slack", curve.slack);
					int steps = EditorGUILayout.IntField("Points", curve.steps);
					Color color = EditorGUILayout.ColorField("Color", curve.color);

					if (EditorGUI.EndChangeCheck())
					{
						Undo.RecordObject(cable, "Edited Cable");
						curve.start = start;
						curve.end = end;
						curve.slack = slack;
						curve.steps = steps;
						curve.color = color;
						setDirty = true;
					}

					if (GUILayout.Button("Clone"))
					{
						Undo.RecordObject(cable, "Cloned Cable");
						cable.cables.Add(new CableCurve(curve));
						count++;
						setDirty = true;
					}

					if (GUILayout.Button("Remove"))
					{
						Undo.RecordObject(cable, "Removed Cable");
						cable.cables.RemoveAt(i);
						count--;
						setDirty = true;
					}
				}

				EditorGUILayout.EndVertical();

				if (r.Contains(Event.current.mousePosition))
				{
					highlight = i;
					setDirty = true;
				}

				EditorGUILayout.Space();
			}
		}

		if (GUILayout.Button("Add Cable"))
		{
			Undo.RecordObject(cable, "Added Cable");
			if (cable.cables == null)
				cable.cables = new List<CableCurve>();
			cable.cables.Add(new CableCurve());
			setDirty = true;
		}

		if (setDirty)
		{
			EditorUtility.SetDirty(cable);
			cable.GenerateMesh();
		}
	}
}