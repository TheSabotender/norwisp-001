using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    public static void DestroyChildren(this Transform transform)
    {
        var childCount = transform.childCount;
        var children = new List<Transform>();
        for (var i = 0; i < childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }

        foreach (var child in children)
        {
            Object.Destroy(child.gameObject);
        }
    }
}
