using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizEnd : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        //if score is bellow 50% then no congratulations
        if (scoreKeeper.GetScore() < 50)
        {
            finalScoreText.text = $"You scored {scoreKeeper.GetScore()}%. Try again!";
            return;
        }
        finalScoreText.text = $"Congratulations!\n You scored {scoreKeeper.GetScore()}%!";
    }

}
