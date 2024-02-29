using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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

    [Header("Radial Cooldown")]
    public Button cooldownButton;
    public Image cooldownRadial;
    [SerializeField] private float timerValue =3f;
    private float cooldownTimer;

    [Header("Dropdown")]
    public GameObject subtitle;
    [SerializeField] float scaleChange = 0.15f;
    [SerializeField] float speed =2f;

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

        cooldownRadial.enabled = false;

        //sets subtitle to random game name
        HandleDropDown(Random.Range(0, 5));
    }

    // Update is called once per frame
    void Update()
    {
        PrintScore();
        UpdateRGBPicture();
        IncrementTimer();
        AnimateSubtitle();
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


    //Cooldown Button
    public void DisableButton()
    {
        cooldownButton.interactable = false;
        cooldownTimer = timerValue;
        cooldownRadial.enabled = true;
    }

    //function for handling timer for button disable
    private void IncrementTimer()
    {
        if (!cooldownButton.interactable)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                cooldownButton.interactable=true;
                cooldownRadial.enabled = false;
            }
        }

        ModifyRadial();
    }

    private void ModifyRadial()
    {
        if (cooldownTimer > 0f)
        {
            //sets fill amount to correct ratio according to time remaining
            cooldownRadial.fillAmount = (cooldownTimer / timerValue);
        }
    }

    //Dropdown Menu
    public void HandleDropDown(int input)
    {
        string gameName = "";

        //sets the name equal to the connected dropdown option
        switch(input)
        {
            case 0:
                gameName = "Super Cool Game";
                break;

            case 1:
                gameName = "Cool Game 64";
                break;

            case 2:
                gameName = "Cool Game";
                break;

            case 3:
                gameName = "UI Demo";
                break;

            case 4:
                gameName = "Menu UI Demo";
                break;

            case 5:
                gameName = "UI Sim 2024";
                break;

            default:
                gameName = "Everything";
                break;
        }

        subtitle.GetComponent<TextMeshProUGUI>().SetText("Better Than: " + gameName + "!!!");
    } 

    //dynamically changes the scale of a UI element
    private void AnimateSubtitle()
    {
        float scale = 1 + scaleChange * (Mathf.Sin(Time.time * speed));
        subtitle.GetComponent<RectTransform>().localScale = new Vector3(scale, scale);
    }
}
