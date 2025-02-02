using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TMP_Text scoreBoard;
    public TMP_Text scoreText;
    public GameObject scoringUI;
    public static int MIN_SCORE = 1;
    private static Dictionary<string, int> scores = new Dictionary<string, int>();

    private int score;

    void Start()
    {  
        userInputField.onSubmit.AddListener(SubmitInput);
        scoringUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SubmitInput(string inputText)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (scores.ContainsKey(inputText)) {
                if (scores[inputText] <= score) {
                    scores[inputText] = score;
                    displayScores();
                }
                displayScores();
                return;
            }
            scores.Add(inputText, score);
            displayScores();
        }
    }

    public void addScore() {
        string input = scoreText.text;
        score = int.Parse(input.Split(' ')[0]); 
        if (score > MIN_SCORE) {
            scoringUI.gameObject.SetActive(true);
            userInputField.Select();
        }
        displayScores();
    }

    private void displayScores() {
        Debug.Log("ahaah");
        String text = "Scores: \n";
        foreach (var ele in scores)
        {
            Debug.Log("fjeiwjoiwa");
            text += $"Name: {ele.Key}, Score: {ele.Value}\n";
        }
        scoreBoard.text = text;
        scoreText.gameObject.SetActive(true);
    }
}
