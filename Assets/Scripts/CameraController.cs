using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _offsetValue = -22f;
    [SerializeField] private float _scrollMultiplier = 1;

    [SerializeField] private float _maxOffsetValue = -10f;
    [SerializeField] private float _minOffsetValue = -60f;

    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    private CinemachineTransposer[] _transposers = new CinemachineTransposer[4];

    private void Start()
    {
        for (int i = 0; i < _transposers.Length; i++)
        {
            _transposers[i] = _virtualCameras[i].GetCinemachineComponent<CinemachineTransposer>();
        }
    }

    private void Update()
    {
        var scrollAmount = _offsetValue + Input.mouseScrollDelta.y * _scrollMultiplier;
        _offsetValue = Mathf.Clamp(scrollAmount, _minOffsetValue, _maxOffsetValue);

        foreach (CinemachineTransposer transposer in _transposers)
        {
            transposer.m_FollowOffset = new Vector3(0, 0, _offsetValue);
        }
    }
}
