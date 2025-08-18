using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve3PointLineRenderer))]
public class CopyBezierArrayToClipboardEditor : Editor
{
    private BezierCurve3PointLineRenderer _bezierComponent;
    private SerializedProperty _lineRenderer;

    private void OnEnable()
    {
        _bezierComponent = (BezierCurve3PointLineRenderer)target;

        if (_bezierComponent.lineRenderer == null)
        {
            _bezierComponent.lineRenderer = _bezierComponent.GetComponent<LineRenderer>();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (_bezierComponent == null)
        {
            return;
        }

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Copy points to clipboard"))
            {
                CopyPath();
            }
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    void CopyPath()
    {
        Vector3[] allPoints = new Vector3[_bezierComponent.lineRenderer.positionCount];

        _bezierComponent.lineRenderer.GetPositions(allPoints);

        var listOfPoints = new List<LinePoint>();

        foreach (var point in allPoints)
        {
            listOfPoints.Add(new LinePoint(point));
        }

        string arrayString = JsonConvert.SerializeObject(listOfPoints);

        EditorGUIUtility.systemCopyBuffer = arrayString;
    }
}