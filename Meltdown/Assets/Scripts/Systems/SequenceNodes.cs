using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceNodes : MonoBehaviour
{
    [Header("All available options")]
    public List<Sprite> symbols;

    [Space(), Header("References")]
    public Image imageScreen;

    [Space(), Header("Sequence Stages")]
    public int curAmount = 4;
    public int lowest = 4;
    public int highest = 10;
    public int intervals = 1;

    [Space(), Header("Interval between repeat")]
    public float minTimeTillReset = 10;
    public float maxTimeTillReset = 15;

    [Space()]
    public List<int> playerChosen = new List<int>();
    List<int> numbers = new List<int>();

    [Space(), Header("Other")]
    public bool success = false;
    public bool completedRepeat = false;

    private void Start()
    {
        StartButtonLoop();
    }

    public void StartButtonLoop()
    {
        numbers.Clear();
        for (int i = 0; i < curAmount; i++)
        {
            numbers[i] = Random.Range(0, symbols.Count);
        }

        playerChosen.Clear();

        StartCoroutine(ButtonSequence(numbers));
    }

    public void UpdatePlayerList(int valueSelected)
    {
        playerChosen.Add(valueSelected);
        if (playerChosen.Count - 1 == numbers.Count)
            completedRepeat = true;
    }

    private IEnumerator ButtonSequence(List<int> numbers)
    {
        for(int i=0; i < numbers.Count; i++)
        {
            imageScreen.sprite = symbols[numbers[i]];
            yield return new WaitForSeconds(intervals);
        }

        yield return new WaitUntil(() => completedRepeat = true);

        int count = 0;

        success = true;

        foreach(int number in numbers)
        {
            if (number != playerChosen[count])
            { success = false; }
            count++;
        }

        // Insert stability change here

        // Wait for random amount of time until restarting the loop

        yield return new WaitForSeconds(Random.Range(minTimeTillReset, maxTimeTillReset));

        StartButtonLoop();

        yield return null;
    }
}
