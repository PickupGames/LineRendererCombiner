using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve3PointLineRenderer : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public LineRenderer lineRenderer;
    public int vertexCount = 12;

    private readonly List<Vector3> _pointList = new List<Vector3>();

    private void Update()
    {
        if (point1 && point2 && point3 && lineRenderer && vertexCount > 0)
        {
            UpdateLine();
        }
    }

    void UpdateLine()
    {
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(point1.position, point2.position, ratio);
            var tangentLineVertex2 = Vector3.Lerp(point2.position, point3.position, ratio);
            var bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            _pointList.Add(bezierpoint);
        }

        lineRenderer.positionCount = _pointList.Count;
        lineRenderer.SetPositions(_pointList.ToArray());

        _pointList.Clear();
    }

    private void OnDrawGizmos()
    {
        if (!(point1 && point2 && point3 && lineRenderer && vertexCount > 0))
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(point1.position, point2.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(point2.position, point3.position);

        Gizmos.color = Color.red;
        for (float ratio = 0.5f / vertexCount; ratio < 1; ratio += 1.0f / vertexCount)
        {
            Gizmos.DrawLine(Vector3.Lerp(point1.position, point2.position, ratio), Vector3.Lerp(point2.position, point3.position, ratio));
        }
    }
}