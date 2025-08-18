using System;
using UnityEngine;

[Serializable]
public class LinePoint
{
    public float x;
    public float y;
    public float z;

    public LinePoint(Vector3 point)
    {
        x = point.x;
        y = point.y;
        z = point.z;
    }

    public Vector3 ConvertToVector3()
    {
        return new Vector3(x, y, z);
    }
}