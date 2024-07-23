using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int questionsAnswered = 0;
    int questionsCorrect = 0;

    public void AddQuestionAnswered(bool isCorrect)
    {
        questionsAnswered++;
        if (isCorrect)
        {
            questionsCorrect++;
        }
    }

    public void ResetScore()
    {
        questionsAnswered = 0;
        questionsCorrect = 0;
    }

    //return questons answered as % of total questions
    public int GetScore()
    {
        if (questionsAnswered == 0)
        {
            return 100;
        }

        return (int)((float)questionsCorrect / questionsAnswered * 100);
    }
}
