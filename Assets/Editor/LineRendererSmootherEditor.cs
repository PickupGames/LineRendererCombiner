using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineRendererSmoother))]
public class LineRendererSmootherEditor : Editor
{
    private LineRendererSmoother _smoother;

    private void OnEnable()
    {
        _smoother = (LineRendererSmoother)target;

        if (_smoother.Line == null) return;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (_smoother == null)
        {
            return;
        }

        EditorGUILayout.BeginHorizontal();
        {
            GUI.enabled = !string.IsNullOrEmpty(_smoother.NewPositions);
            if (GUILayout.Button("Add Path"))
            {
                AddPath();
            }
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }


    private void AddPath()
    {
        if (string.IsNullOrWhiteSpace(_smoother.NewPositions)) return;

        var currentIndex = _smoother.Line.positionCount;

        var previousPoints = new Vector3[currentIndex];

        _smoother.Line.GetPositions(previousPoints);


        var newPoints = JsonConvert.DeserializeObject<List<LinePoint>>(_smoother.NewPositions);

        Undo.RecordObject(_smoother.Line, "Add Path");

        _smoother.Line.positionCount += newPoints.Count;

        foreach (var newPoint in newPoints)
        {
            _smoother.Line.SetPosition(currentIndex, newPoint.ConvertToVector3());
            currentIndex++;
        }

        _smoother.NewPositions = string.Empty;
        EditorUtility.SetDirty(_smoother.Line);
    }
}