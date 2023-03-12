using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarWaypoint : MonoBehaviour
{
    public enum WaypointCategory
    {
        Unknown = -1,
        Star,
        ConstructionArea,
        PartsArea,
    }

    [SerializeField] private WaypointCategory _waypointCategory;

    public WaypointCategory Category => _waypointCategory;
}
