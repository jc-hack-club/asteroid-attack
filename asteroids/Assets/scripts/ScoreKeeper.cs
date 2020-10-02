using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private static ScoreKeeper _instance;

    public Text scoreText;
    public string scoreTextPrefix = "SCORE: ";

    private void Update()
    {
        scoreText.text = scoreTextPrefix + CalculateScore();
        Debug.Log("score: " + CalculateScore());
    }

    public static ScoreKeeper Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ScoreKeeper>();
            }

            return _instance;
        }
    }

    public int destroyedAsteroids = 0;
    public float startTime;

    public void Start()
    {
        startTime = Time.time;
    }

    public void IncrementDestroyedAsteroids()
    {
        destroyedAsteroids++;
    }

    public long CalculateScore()
    {
        return (long)(Time.time - startTime) + (100 * destroyedAsteroids);
    }
}
