using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomGravity
{
    const float maxGravityDistance = 40f;
    private static Dictionary<Transform, Vector4> gravityWells;

    public static Vector3 GetDirection(Vector3 objectPosition)
    {
        if (gravityWells == null)
            gravityWells = new Dictionary<Transform, Vector4>();

        Vector3 dir = objectPosition;
        float pow = 1;

        Vector3 simpleCenterOfGravity = Vector3.zero;

        foreach(Transform g in gravityWells.Keys)
        {
            dir = Vector3.MoveTowards(dir, gravityWells[g], gravityWells[g].w);
            if (gravityWells[g].w > pow)
                pow = gravityWells[g].w;

            simpleCenterOfGravity += new Vector3(gravityWells[g].x, gravityWells[g].y, gravityWells[g].z);
        }
        simpleCenterOfGravity /= gravityWells.Keys.Count;

        var gravityDistance = (objectPosition - simpleCenterOfGravity);
        var gravityForce = 0.25f + (1f - Mathf.Min(maxGravityDistance, gravityDistance.magnitude) / maxGravityDistance) * 4f;

        return Vector3.Normalize(objectPosition - dir) * (pow * gravityForce);
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
