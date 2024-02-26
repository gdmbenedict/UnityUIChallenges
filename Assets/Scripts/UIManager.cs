using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score;

    // Update is called once per frame
    void Update()
    {
        PrintScore();
    }

    public void ModifyScore(int value)
    {
        if (value == 0)
        {
            score = 0;
        }

        if ((value > 0 && score < 999999999) || value < 0 && score > -999999999)
        {
            score += value;
        }
        
    }

    private void PrintScore()
    {
        string scoreText = "Score: " + score.ToString("N0");
        this.scoreText.SetText(scoreText);
    }
}
