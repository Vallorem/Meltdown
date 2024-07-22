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
    private void Start()
    {
        currentStability = stabilityMax;
        currentPressure = pressureMax;
    }

    public void UpdateStability(bool successfulUpdate)
    {
        if (successfulUpdate)
            stabilityMax += rewardAddition;
        else
            stabilityMax -= penaltyReducement;
    }

    private void Update()
    {
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
