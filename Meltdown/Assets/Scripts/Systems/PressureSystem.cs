using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PressureSystem : MonoBehaviour
{
    [Header("Time for each bar without influence (in Seconds)")]
    [SerializeField] private float stabilityMax = 480;
    [SerializeField] private float pressureMax = 600;

    [Space(), Header("Penalties/Rewards")]
    [SerializeField] private float penaltyReducement = 20;
    [SerializeField] private float rewardAddition = 20;

    [SerializeField] private float decreasePerPuzzle = 1;

    [Space(), Header("References")]
    [SerializeField] private RectTransform PressureBar;
    [SerializeField] private RectTransform StabilityBar;

    [SerializeField] private GameObject GameOverScreen;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI completetext;
    [SerializeField] private TextMeshProUGUI failtext;

    [Space(), Header("Appropriate values")]
    public float currentPuzzlesUncompleted = 0;

    [HorizontalLine(2, EColor.White), Header("Play Time Variables")]
    [SerializeField] private float currentStability;
    [SerializeField] private float currentPressure;

    private float totalTime = 0;
    private int puzzlesSucceeded = 0;
    private int puzzlesFailed = 0;
    public float currentBasePressureDrop = 1.5f;
    public float amountOfPressureUntouched = 4;

    public Light light1;
    public Light light2;
    
    public AudioSource alarm;
    public AudioSource wrong;
    public AudioSource right;
    
    private bool startedAlarm;
    public void EndGame()
    {
        currentPressure = 0;
        currentStability = 0;
    }
    private void Start()
    {
        currentStability = stabilityMax;
        currentPressure = pressureMax;
    }

    public void UpdateStability(bool successfulUpdate)
    {
        if (successfulUpdate)
        { currentStability += rewardAddition; currentPuzzlesUncompleted -= 1; puzzlesSucceeded++; right.Play();}
        else
        { currentStability -= penaltyReducement; puzzlesFailed++; wrong.Play(); }

        if (currentStability > stabilityMax)
            currentStability = stabilityMax;
    }

    private void Update()
    {
        if(amountOfPressureUntouched < 0)
            amountOfPressureUntouched = 0;
        
        if(amountOfPressureUntouched > 4)
            amountOfPressureUntouched = 4;
        
        if(currentStability <= 0)
        {
            currentStability = 0;
        }
        else
        {
            currentStability -= (1 * (decreasePerPuzzle * currentPuzzlesUncompleted + 1)) * Time.deltaTime;
            StabilityBar.offsetMin = new Vector2(1 - (currentStability / stabilityMax), 0);
        }

        if(currentStability < 200 && !startedAlarm)
        {
            startedAlarm = true;
            light1.color = Color.red;
            light2.color = Color.red;
            alarm.Play();
        }
        
        if(currentPressure > 0)
        {
            totalTime += Time.deltaTime;
            currentPressure -= Time.deltaTime * (currentBasePressureDrop + (.5f - .125f * amountOfPressureUntouched)) * (stabilityMax / currentStability);
            PressureBar.offsetMin = new Vector2(1 - (currentPressure / pressureMax), 0);
        }
        else
        {
            GameOverScreen.SetActive(true);
            time.text = "Time Survived: " + totalTime;
            completetext.text = "Puzzles Complete: " + puzzlesSucceeded;
            failtext.text = "Puzzles Failed: " + puzzlesFailed;
        }

    }
}
