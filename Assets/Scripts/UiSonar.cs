using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UiSonar : MonoBehaviour
{
    [SerializeField] private PlayerControls _player;
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private RectTransform _waypointVisualParent;
    [SerializeField] private UiWaypointVisual _waypointVisualPrefab;

    [SerializeField] private float _tooCloseThreshold = 25f;

    private List<UiWaypointVisual> _activeWaypointVisuals;

    private void Start()
    {
        CreateWaypointVisuals();
    }

    private void LateUpdate()
    {
        UpdateWaypointVisuals();
    }

    private void CreateWaypointVisuals()
    {
        _waypointVisualParent.DestroyChildren();

        var waypoints = FindObjectsOfType<SonarWaypoint>().ToList();
        _activeWaypointVisuals = new List<UiWaypointVisual>();
        foreach (var waypoint in waypoints)
        {
            var waypointVisual = Instantiate(_waypointVisualPrefab, _waypointVisualParent);
            waypointVisual.Initialize(waypoint);
            _activeWaypointVisuals.Add(waypointVisual);
        }
    }

    private void UpdateWaypointVisuals()
    {
        var playerPos = _player.transform.position;
        var cameraAngle = _mainCamera.transform.rotation.eulerAngles.z;
        foreach (var waypointVisual in _activeWaypointVisuals)
        {
            var waypointPos = waypointVisual.SonarWaypoint.transform.position;

            var direction = (waypointPos - playerPos).normalized;
            var distance = Vector3.Distance(playerPos, waypointPos);

            waypointVisual.gameObject.SetActive(distance > _tooCloseThreshold);

            var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90 - cameraAngle;
            waypointVisual.transform.rotation = Quaternion.Euler(0,0,angle);

            var waypointCategory = waypointVisual.SonarWaypoint.Category;
            var arrowScale = Mathf.Lerp(1, 0.2f, distance / 100);

            waypointVisual.SetArrowScale(arrowScale);
            waypointVisual.SetColor(GetCategoryColor(waypointCategory).WithAlpha(1));
            waypointVisual.SetText($"{waypointCategory}: {distance:N0}");
        }
    }

    private Color GetCategoryColor(SonarWaypoint.WaypointCategory category)
    {
        switch (category)
        {
            case SonarWaypoint.WaypointCategory.Unknown: return Color.grey;
            case SonarWaypoint.WaypointCategory.Star: return new Color(1, 0.25f, 0);
            case SonarWaypoint.WaypointCategory.ConstructionArea: return new Color(1, 0.75f, 0);
            case SonarWaypoint.WaypointCategory.PartsArea: return new Color(0, 0.5f, 1);
            default: return Color.magenta;
        }
    }


    private void OnDrawGizmos()
    {
        if(_activeWaypointVisuals == null || _activeWaypointVisuals.Count == 0)
            return;

        Gizmos.color = Color.green;
        var playerPos = _player.transform.position;
        Gizmos.DrawSphere(playerPos, 1);


        foreach (var waypointVisual in _activeWaypointVisuals)
        {
            var waypointPos = waypointVisual.SonarWaypoint.transform.position;

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(waypointPos, 1);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(playerPos, waypointPos);
        }
    }

}
