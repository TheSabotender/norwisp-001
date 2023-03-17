using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGod : MonoBehaviour
{
    [SerializeField] private GameObject[] _virtualCameras;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private TextMeshProUGUI _timerText;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    private int _cameraSettingVal;
    private float _gameTimer = 180.4f;


    public void Start()
    {
        BonusObject.OnPointScored += OnPointScored;
        DropZone.CargoScored += OnCargoScored;
        DropZone.SuperCargoScored += OnSuperCargoScored;

        _cameraSettingVal = PlayerPrefs.GetInt($"CameraSetting");
        UpdateSelectedCamera();

        UpdateTimerText();
    }

    private void OnDestroy()
    {
        BonusObject.OnPointScored -= OnPointScored;
    }


    void OnCargoScored()
    {
        score += 10;
    }
    void OnSuperCargoScored()
    {
        score += 30;
    }
    void OnPointScored()
    {
        score++;
    }


    // Update is called once per frame
    private void Update()
    {
        scoreText.text = $"{score} points";

        _gameTimer -= Time.deltaTime;
        UpdateTimerText();
        if (_gameTimer <= 0.0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //reboot/respawn
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _cameraSettingVal++;
            if (_cameraSettingVal >= 4)
            {
                _cameraSettingVal = 0;
            }

            PlayerPrefs.SetInt($"CameraSetting", _cameraSettingVal);
            UpdateSelectedCamera();
        }
    }

    private void UpdateSelectedCamera()
    {
        for (int i = 0; i < _virtualCameras.Length; i++)
        {
            _virtualCameras[i].SetActive(_cameraSettingVal == i);
        }

        SetTutorialText();
    }

    private void SetTutorialText()
    {
        _tutorialText.text =
            $"Left Mouse / A\nRight Mouse / D\nSpace Load / Unload\nR restart\nC camera setting [{_cameraSettingVal + 1}/{_virtualCameras.Length}]";
    }

    private void UpdateTimerText()
    {
        _timerText.text = $"TIME: {_gameTimer.ToString("000")}";
    }

    private void FixedUpdate()
    {
    }

}