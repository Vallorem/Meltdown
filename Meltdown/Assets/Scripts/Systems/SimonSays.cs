using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class SimonPrompts
{
    public string prompt;
    public bool pressSwitch1;
    public bool pressSwitch2;

    public bool simonSaid;
}

public class SimonSays : MonoBehaviour
{
    [Header("Switch 1")]
    [SerializeField] private Animator animator1;
    public bool pressedSwitch1 = false;

    [Space(), Header("Switch 2")]
    [SerializeField] private Animator animator2;
    public bool pressedSwitch2 = false;

    [Space(), Header("Main Screen")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private List<SimonPrompts> prompts = new List<SimonPrompts>();

    [Space(), Header("Variables")]
    [SerializeField] private float playLength;
    [SerializeField] private float minCooldown;
    [SerializeField] private float maxCooldown;

    private PressureSystem stability;
    private bool actionTaken = false;
    private bool success = false;

    private bool firstPass = true;
    private void Start()
    {
        stability = FindFirstObjectByType<PressureSystem>();

        SimonSaysGameLoop();
    }

    public void PullLever(int whichLever)
    {
        print("pulled");
        if (whichLever == 1)
            pressedSwitch1 = true;
        if (whichLever == 2)
            pressedSwitch2 = true;
    }

    private void SimonSaysGameLoop()
    {
        if (success)
            stability.currentPuzzlesUncompleted += 1;

        pressedSwitch1 = false;
        pressedSwitch2 = false;
        success = false;
        actionTaken = false;

        SimonPrompts promptTaken = prompts[Random.Range(0, prompts.Count - 1)];
        if (stability != null)
            StartCoroutine(DisplayPrompt(promptTaken));
    }

    private IEnumerator DisplayPrompt(SimonPrompts prompt)
    {
        if (firstPass)
            yield return new WaitForSeconds(Random.Range(5, 10));

        firstPass = false;
        // Ensure the action is always taken even if a dud round
        StartCoroutine(EnsureAction());
        if (prompt.simonSaid)
        { text.text = "Action To Complete"; yield return new WaitForSeconds(1.5f); }

        text.text = prompt.prompt;
        yield return new WaitForSeconds(3);
        text.text = "Pull Lever";

        yield return new WaitUntil(() => actionTaken);

        bool completedTask = true;

        if (prompt.pressSwitch1 != pressedSwitch1)
            completedTask = false;

        if (prompt.pressSwitch2 != pressedSwitch2)
            completedTask = false;

        if (completedTask)
            text.text = "Prompt Completed";
        else
            text.text = "Prompt Failed";

        stability.UpdateStability(completedTask);
        if (completedTask) success = true;

        yield return new WaitForSeconds(Random.Range(minCooldown, maxCooldown));
        SimonSaysGameLoop();
        yield return null;
    }

    private IEnumerator EnsureAction()
    {
        yield return new WaitForSeconds(playLength);
        if (!actionTaken)
            actionTaken = true;
    }
}
