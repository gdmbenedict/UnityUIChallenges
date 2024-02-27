using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Score Fields")]
    public TextMeshProUGUI scoreText;

    [SerializeField] int maxScore = 999999999;
    [SerializeField] int minScore = 0;

    [SerializeField] int maxRandomScore = 999999999;
    [SerializeField] int minRandomScore = -999999999;

    private int score;

    [Header("Element Hiding")]
    public GameObject hidingImage;
    public TextMeshProUGUI hideButtonText;

    //start function called on first frame
    void Start()
    {
        hidingImage.SetActive(false);
        SetHideButtonText();
    }

    // Update is called once per frame
    void Update()
    {
        PrintScore();
    }

    //Score Handling functions

    public void ModifyScore(int value)
    {
        if (value == 0)
        {
            score = 0;
        }

        score += value;

        CheckBoundires();
    }

    public void RandomScore()
    {
        score += Random.Range(minRandomScore, maxRandomScore);

        CheckBoundires();
    }

    private void CheckBoundires()
    {
        score = Mathf.Clamp(score, minScore, maxScore);
    }

    private void PrintScore()
    {
        string scoreText = "Score: " + score.ToString("N0");
        this.scoreText.SetText(scoreText);
    }

    //Show / Hide Image functions
    
    public void ToggleHiding()
    {
        hidingImage.SetActive(!hidingImage.activeInHierarchy);
        SetHideButtonText();
    }

    private void SetHideButtonText()
    {
        string buttonAction;

        if (hidingImage.activeInHierarchy)
        {
            buttonAction = "Decline";
        }
        else
        {
            buttonAction = "Accept";
        }

        hideButtonText.text = buttonAction + " Cookies";
    }
}
