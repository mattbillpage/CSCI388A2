using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SimonSaysManager : MonoBehaviour
{
    [Header("UI Display")]
    public TextMeshProUGUI statusText;

    [Header("Puzzle Components")]
    public MeshRenderer[] buttonRenderers;
    public Material[] litMaterials;
    public Material[] unlitMaterials;

    [Header("Game Settings")]
    public int sequenceLength = 5;
    public float flashDuration = 0.5f;
    public float pauseBetweenFlashes = 0.2f;

    [Header("Hover Settings")]
    public float hoverDelay = 0.8f; // Seconds required to hold the laser on a ball
    private Coroutine hoverCoroutine;

    private List<int> currentSequence = new List<int>();
    private int playerInputIndex = 0;
    private bool isPlayerTurn = false;

    void Start()
    {
        // Start the puzzle as soon as the game loads
        statusText.text = "SYSTEM ONLINE";
        statusText.color = Color.white;
        StartCoroutine(StartPuzzleWithDelay());
    }

 
    IEnumerator StartPuzzleWithDelay()
    {
        yield return new WaitForSeconds(2f); // Give player time to load in
        StartPuzzle();
    }

    public void StartPuzzle()
    {
        currentSequence.Clear();
        currentSequence.Add(Random.Range(0, buttonRenderers.Length)); // Pick first random color
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        isPlayerTurn = false;
        statusText.color = Color.white;
        statusText.text = "WATCH THE PATTERN...";

        yield return new WaitForSeconds(1f);

        // Flash each button in the current sequence
        foreach (int index in currentSequence)
        {
            buttonRenderers[index].material = litMaterials[index];
            yield return new WaitForSeconds(flashDuration);

            buttonRenderers[index].material = unlitMaterials[index];
            yield return new WaitForSeconds(pauseBetweenFlashes);
        }

        statusText.text = "YOUR TURN";
        isPlayerTurn = true;
        playerInputIndex = 0; // Reset player's place in the pattern
    }

    // --- GAZE DELAY LOGIC ---

    public void OnHoverEnter(int buttonIndex)
    {
        if (!isPlayerTurn) return;

        // Stop any existing timer just to be safe
        if (hoverCoroutine != null) StopCoroutine(hoverCoroutine);

        // Start the countdown timer
        hoverCoroutine = StartCoroutine(HoverTimerRoutine(buttonIndex));
    }

    public void OnHoverExit()
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine); // Cancel the click if the laser leaves early
            hoverCoroutine = null;
        }
    }

    IEnumerator HoverTimerRoutine(int buttonIndex)
    {
        // Wait for the specified amount of time (0.8 seconds default)
        yield return new WaitForSeconds(hoverDelay);

        // If the laser is still here after the delay, trigger the actual click
        OnButtonPressed(buttonIndex);
    }

    // --- CORE GAMEPLAY LOGIC ---

    public void OnButtonPressed(int buttonIndex)
    {
        if (!isPlayerTurn) return; // Ignore input if the game is still showing the pattern

        StartCoroutine(FlashSingleButton(buttonIndex));

        // Check if they selected the correct ball
        if (buttonIndex == currentSequence[playerInputIndex])
        {
            playerInputIndex++;

            // Did they finish the current round?
            if (playerInputIndex >= currentSequence.Count)
            {
                isPlayerTurn = false;

                // Did they hit 5 in a row and win the whole game?
                if (currentSequence.Count >= sequenceLength)
                {
                    StartCoroutine(WinRoutine());
                }
                else
                {
                    // Add a new color to the pattern and start the next round
                    currentSequence.Add(Random.Range(0, buttonRenderers.Length));
                    StartCoroutine(PlaySequence());
                }
            }
        }
        else
        {
            // They messed up the pattern
            StartCoroutine(FailRoutine());
        }
    }

    IEnumerator FlashSingleButton(int index)
    {
        // Quick visual flash when the ball is registered as "clicked"
        buttonRenderers[index].material = litMaterials[index];
        yield return new WaitForSeconds(0.2f);
        buttonRenderers[index].material = unlitMaterials[index];
    }

    IEnumerator FailRoutine()
    {
        isPlayerTurn = false;
        statusText.color = Color.red;
        statusText.text = "WRONG! RESETTING...";

        yield return new WaitForSeconds(2f); // Wait so they can read it
        StartPuzzle();
    }

    IEnumerator WinRoutine()
    {
        isPlayerTurn = false;
        statusText.color = Color.green;
        statusText.text = "ACCESS GRANTED";

        // Wait 3 seconds so they can celebrate
        yield return new WaitForSeconds(3f);

        // Teleport the player to the new scene!
        SceneManager.LoadScene("CelebrationRoom");
    }
}