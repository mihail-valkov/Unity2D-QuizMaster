using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Category("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] ScoreKeeper scoreKeeper;
    [Category("UI")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] ImageSign[] answerImages;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider progressSlider;

    [Category("Sprites")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite incorrectAnswerSprite;
    [SerializeField] Sprite unansweredSprite;
    
    [Category("Gameplay")]
    [SerializeField] float timeToAnswer = 30f;
    [SerializeField] Timer timer;

    QuestionSO currentQuestion;
    int totalNumberOfQuestions;

    void Start()
    {
        totalNumberOfQuestions = questions.Count;
        progressSlider.maxValue = totalNumberOfQuestions;

        NextQuestion();

        timer.TimerFinished += OnTimerFinished;
    }

    //unsubscribe from events
    private void OnDestroy()
    {
        timer.TimerFinished -= OnTimerFinished;
    }

    private void OnTimerFinished()
    {
        OnAnswerSelected(-1);
    }

    private void LoadQuestion()
    {
        progressSlider.value = totalNumberOfQuestions - questions.Count; 
        
        questionText.text = currentQuestion.Question;

        //find the textmeshpro text object within the answerbuttons and assignthe text from the question.Answers array
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.Answers[i];
            answerButtons[i].GetComponent<Image>().sprite = unansweredSprite;
        }

        //reset the image signs
        for (int i = 0; i < answerImages.Length; i++)
        {
            answerImages[i].ResetImage();
        }

        //start the timer
        timer.StartTimer(timeToAnswer);

        SetButtonsState(true);
    }

    //set the buttons interactable state
    void SetButtonsState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            //set button interactable
            answerButtons[i].GetComponent<Button>().interactable = state;
        }
    }

    public void OnAnswerSelected(int index)
    {
        SetButtonsState(false);

        //stop timer
        timer.StopTimer();

        //reveal if the anser is correct for all buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i == currentQuestion.CorrectAnswerIndex)
            {
                answerButtons[i].GetComponent<Image>().sprite = correctAnswerSprite;
            }
            else
            {
                answerButtons[i].GetComponent<Image>().sprite = incorrectAnswerSprite;
            }
        }

        if (index == currentQuestion.CorrectAnswerIndex)
        {
            answerImages[index].SetCorrect();
        }
        else if (index >= 0 && index < currentQuestion.Answers.Length)
        {
            answerImages[index].SetIncorrect();
        }

        scoreKeeper.AddQuestionAnswered(index == currentQuestion.CorrectAnswerIndex);

        //change score text glowcolor value from red through orange to green depending or the score from 0 to 100
        scoreText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, 
            Color.Lerp(Color.red, new Color(0, 1, 0.25f, 0.5f), scoreKeeper.GetScore() / 100f));

        scoreText.text = $"Score: { scoreKeeper.GetScore() }%";

        //change question after 2 seconds
        StartCoroutine(ChangeQuestionCR());
    }

    IEnumerator ChangeQuestionCR()
    {
        yield return new WaitForSeconds(2);

        NextQuestion();
    }

    private void NextQuestion()
    {
        if (questions.Count == 0)
        {
            // show the end screen
            FindObjectOfType<GameManager>().EndQuiz();    
            return;
        }

        SelectRandomQuestion();
        LoadQuestion();
    }

    //select randomquestion and remove it from the list
    void SelectRandomQuestion()
    {
        if (questions.Count == 0)
        {
            return;
        }
        
        int randomIndex = UnityEngine.Random.Range(0, questions.Count);
        this.currentQuestion = questions[randomIndex];
        questions.RemoveAt(randomIndex);
    }
}
