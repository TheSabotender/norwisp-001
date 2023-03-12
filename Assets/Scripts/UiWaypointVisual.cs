using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiWaypointVisual : MonoBehaviour
{
    [SerializeField] private RectTransform _arrowRectTransform;
    [SerializeField] private Image _arrowImage;
    [SerializeField] private TextMeshProUGUI _waypointText;

    public SonarWaypoint SonarWaypoint { get; private set; }

    public void Initialize(SonarWaypoint sonarWaypoint)
    {
        SonarWaypoint = sonarWaypoint;
    }

    public void SetColor(Color color)
    {
        _arrowImage.color = color;
        _waypointText.color = color;
    }

    public void SetArrowScale(float scale)
    {
        _arrowRectTransform.localScale = Vector3.one * scale;
    }

    public void SetText(string text)
    {
        _waypointText.text = text;
    }
}
