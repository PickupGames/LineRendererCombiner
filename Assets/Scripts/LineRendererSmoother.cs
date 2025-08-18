using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererSmoother : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [TextArea] public string newPositions;

    public LineRenderer Line => line;

    public string NewPositions
    {
        get => newPositions;
        set => newPositions = value;
    }
}