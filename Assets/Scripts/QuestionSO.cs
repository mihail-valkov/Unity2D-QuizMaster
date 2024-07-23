using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question", order = 0)]
public class QuestionSO : ScriptableObject 
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter your question here.";

    //array of 4 answers
    [SerializeField] string[] answers = new string[4];
    //array of correct answer indexes
    [SerializeField] int correctAnswerIndex;
    

    //getter for question
    public string Question
    {
        get { return question; }
    }

    //getter for answers   
    public string[] Answers
    {
        get { return answers; }
    }

    //getter for correct answers
    public int CorrectAnswerIndex
    {
        get { return correctAnswerIndex; }
    }
}
