using UnityEngine;

public class BezierCurve
{
    public static Vector3 ComputeQuadraticBezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 middlePoint, float t)
    {
        // Clamp t between 0 and 1 to ensure the point lies within the curve
        t = Mathf.Clamp01(t);

        // Compute the blending factors
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        // Compute the position on the curve using the quadratic Bezier formula
        Vector3 pointOnCurve = uu * startPoint + 2 * u * t * middlePoint + tt * endPoint;

        return pointOnCurve;
    }
}
