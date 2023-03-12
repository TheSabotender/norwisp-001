using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _offsetValue = -10f;
    [SerializeField] private float _scrollMultiplier = 1;

    [SerializeField] private float _maxOffsetValue = -10f;
    [SerializeField] private float _minOffsetValue = -30f;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private CinemachineTransposer _transposer;

    private void Start()
    {
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        var scrollAmount = _offsetValue + Input.mouseScrollDelta.y * _scrollMultiplier;
        _offsetValue = Mathf.Clamp(scrollAmount, _minOffsetValue, _maxOffsetValue);

        _transposer.m_FollowOffset = new Vector3(0, 0, _offsetValue);
    }
}
