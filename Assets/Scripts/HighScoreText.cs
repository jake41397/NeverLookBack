using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreText : MonoBehaviour
{
    Text HighScore;

    void OnEnable()
    {
        HighScore = GetComponent<Text>();
        HighScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
