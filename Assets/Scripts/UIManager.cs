using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("RGB Sliders")]
    public Image rgbImage;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Slider alphaSlider;

    //start function called on first frame
    void Start()
    {
        hidingImage.SetActive(false);
        SetHideButtonText();

        //setting value of sliders to 1
        redSlider.value = 1;
        greenSlider.value = 1;
        blueSlider.value = 1;
        alphaSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PrintScore();
        UpdateRGBPicture();
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

    //Exit Button
    public void Exit()
    {
        Application.Quit();
    }

    //RGB Slider
    private void UpdateRGBPicture()
    {
        Color color = rgbImage.color;

        color.r = redSlider.value;
        color.g = greenSlider.value;
        color.b = blueSlider.value;
        color.a = alphaSlider.value;

        rgbImage.color = color;
    }
}
