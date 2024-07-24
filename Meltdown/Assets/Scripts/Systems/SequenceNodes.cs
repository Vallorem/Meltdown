using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceNodes : MonoBehaviour
{
    private PressureSystem pressure;

    [Header("All available options")]
    public List<Sprite> symbols;

    [Space(), Header("References")]
    public Image imageScreen;

    public Sprite loading;
    public Sprite flash;
    [Space(), Header("Sequence Stages")]
    public int curAmount = 4;
    public int lowest = 4;
    public int highest = 10;
    public int intervals = 1;

    [Space(), Header("Interval between repeat")]
    public float minTimeTillReset = 10;
    public float maxTimeTillReset = 15;
    
    public AudioSource audio;
    

    [Space()]
    public List<int> playerChosen = new List<int>();
    public List<int> numbers = new List<int>();

    [Space(), Header("Other")]
    public bool success = true;
    public bool completedRepeat = false;

    private bool showing = false;
    private bool playing = false;

    private bool firstPass = true;
    private void Start()
    {
        pressure = FindFirstObjectByType<PressureSystem>();
        StartButtonLoop();
    }

    public void StartButtonLoop()
    {
        playing = true;

        if (success)
        { pressure.currentPuzzlesUncompleted += 1; success = false; }

        numbers.Clear();
        for (int i = 0; i < curAmount; i++)
        {
            numbers.Add(Random.Range(0, symbols.Count - 1));
        }

        playerChosen.Clear();

        showing = true;
        StartCoroutine(ButtonSequence(numbers));
    }

    public void UpdatePlayerList(int valueSelected)
    {
        audio.Play();
        playerChosen.Add(valueSelected);
        if (playerChosen.Count - 1 == numbers.Count - 1)
            completedRepeat = true;
    }

    public void RecallButtonSequence()
    {
        if (!showing && playing)
            showing = true;

        StartCoroutine(DisplaySequence(numbers));
    }

    private IEnumerator DisplaySequence(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            imageScreen.sprite = symbols[numbers[i]];
            yield return new WaitForSeconds(intervals);
            imageScreen.sprite = flash;
            yield return new WaitForSeconds(.1f);
        }
        imageScreen.sprite = loading;
        showing = false;
        yield return null;
    }

    private IEnumerator ButtonSequence(List<int> numbers)
    {
        if (firstPass)
        { pressure.currentPuzzlesUncompleted -= 1; yield return new WaitForSeconds(Random.Range(20, 25)); pressure.currentPuzzlesUncompleted += 1; }

        StartCoroutine(DisplaySequence(numbers));
        yield return new WaitUntil(() => completedRepeat);

        int count = 0;

        success = true;

        foreach(int number in numbers)
        {
            if(numbers.Count != 0)
            {
                if (number != playerChosen[count])
                { success = false; }
                count++;
            }
        }

        // Insert stability change here
        pressure.UpdateStability(success);
        // Wait for random amount of time until restarting the loop
        playing = false;
        completedRepeat = false;
        yield return new WaitForSeconds(Random.Range(minTimeTillReset, maxTimeTillReset));

        StartButtonLoop();

        yield return null;
    }
}
