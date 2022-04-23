using UnityEngine;

public static class Extensions
{
    public static Vector3 GetDirection(ForwardAxis forwardAxis, Transform transform)
    {
        Vector3 dir = Vector3.zero;

        switch (forwardAxis)
        {
            case ForwardAxis.ZAxis:
                dir = transform.forward;
                break;
            case ForwardAxis.XAxis:
                dir = transform.right;
                break;
            case ForwardAxis.YAxis:
                dir = transform.up;
                break;
            default:
                break;
        }

        return dir;
    }

    public enum ForwardAxis
    {
        ZAxis, XAxis, YAxis
    }
}
