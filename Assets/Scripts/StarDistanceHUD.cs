using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarDistanceHUD : MonoBehaviour
{
    [SerializeField] private PlayerControls _player;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _starPosition;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private Image _distancePointer;

    private void LateUpdate()
    {
        UpdateStarPositionVisuals();
    }

    private void UpdateStarPositionVisuals()
    {
        var playerPos = _player.transform.position;
        var cameraAngle = _mainCamera.transform.rotation.eulerAngles.z;

        var direction = (_starPosition.position - playerPos).normalized;
        var distance = Vector3.Distance(playerPos, _starPosition.position);
        _distanceText.text = $"{distance.ToString("0")} M";

        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90 - cameraAngle;
        _distancePointer.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
