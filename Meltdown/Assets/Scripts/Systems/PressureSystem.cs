using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float baseMultiplierForPressure = 1;

    [Space(), Header("References")]
    [SerializeField] private RectTransform PressureBar;
    [SerializeField] private RectTransform StabilityBar;

    [Space(), Header("Appropriate values")]
    public float currentPuzzlesUncompleted = 0;

    [HorizontalLine(2, EColor.White), Header("Play Time Variables")]
    [SerializeField] private float currentStability;
    [SerializeField] private float currentPressure;

    private float totalTime = 0;
    private int puzzlesSucceeded = 0;
    private int puzzlesFailed = 0;

    private void Start()
    {
        currentStability = stabilityMax;
        currentPressure = pressureMax;
    }

    public void UpdateStability(bool successfulUpdate)
    {
        if (successfulUpdate)
        { currentStability += rewardAddition; currentPuzzlesUncompleted -= 1; puzzlesSucceeded++; }
        else
        { currentStability -= penaltyReducement; puzzlesFailed++; }

        if (currentStability > stabilityMax)
            currentStability = stabilityMax;
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
        if(currentStability <= 0)
        {
            currentStability = 0;
        }
        else
        {
            currentStability -= (1 * (decreasePerPuzzle * currentPuzzlesUncompleted + 1)) * Time.deltaTime;
            StabilityBar.offsetMin = new Vector2(1.0645f - (currentStability / stabilityMax), 0);
        }

        if(currentPressure > 0)
        {
            currentPressure -= Time.deltaTime * baseMultiplierForPressure * (stabilityMax / currentStability);
            PressureBar.offsetMin = new Vector2(1.0645f - (currentPressure / pressureMax), 0);
        }

    }
}
