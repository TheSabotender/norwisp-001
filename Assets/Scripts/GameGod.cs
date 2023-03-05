using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameGod : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;


    public void Start()
    {
        BonusObject.OnPointScored += OnPointScored;
        DropZone.CargoScored += OnCargoScored;
    }

    private void OnDestroy()
    {
        BonusObject.OnPointScored -= OnPointScored;
    }


    void OnCargoScored()
    {
        score+=10;
    }
    void OnPointScored()
    {
        score++;
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{score} points";
    }

    private void FixedUpdate()
    {
    }

}