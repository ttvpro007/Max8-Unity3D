//from video at https://youtu.be/6C1NPy321Nk
using UnityEngine;

public class Sinewave : MonoBehaviour
{
    public LineRenderer myLineRenderer;
    [Min(2)]
    public int points = 2;
    public float amplitude = 1;
    [Range(20, 20000)]
    public float frequency = 20;
    public Vector2 xLimits = new Vector2(0, 1);
    public float movementSpeed = 1;
    [Range(0f, 2 * Mathf.PI)]
    public float radians;
    void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
    }

    void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        myLineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));
            myLineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    void Update()
    {
        Draw();
    }
}