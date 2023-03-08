using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomGravity
{
    private static Dictionary<Transform, Vector4> gravityWells;

    public static Vector3 GetDirection(Vector3 objectPosition)
    {
        if (gravityWells == null)
            gravityWells = new Dictionary<Transform, Vector4>();

        Vector3 dir = objectPosition;
        float pow = 1;

        foreach(Transform g in gravityWells.Keys)
        {
            dir = Vector3.MoveTowards(dir, gravityWells[g], gravityWells[g].w);
            if (gravityWells[g].w > pow)
                pow = gravityWells[g].w;
        }

        return Vector3.Normalize(objectPosition - dir) * pow; //TODO "pow" should be stronger the closer you get....
    }

    public static void AddWell(Transform transform, float power)
    {
        if (gravityWells == null)
            gravityWells = new Dictionary<Transform, Vector4>();

        gravityWells[transform] = new Vector4(transform.position.x, transform.position.y, transform.position.z, power);
    }

    public static void RemoveWell(Transform transform)
    {
        if (gravityWells == null)
            gravityWells = new Dictionary<Transform, Vector4>();

        gravityWells.Remove(transform);
    }
}
