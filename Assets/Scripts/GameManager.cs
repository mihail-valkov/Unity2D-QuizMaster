using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    QuizEnd quizEnd;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        quizEnd = FindObjectOfType<QuizEnd>();
    }

    // Start is called before the first frame update
    void Start()
    {
        quiz.gameObject.SetActive(true);
        quizEnd.gameObject.SetActive(false);
    }

    public void EndQuiz()
    {
        quiz.gameObject.SetActive(false);
        quizEnd.gameObject.SetActive(true);
        quizEnd.ShowFinalScore();
    }

    public void RestartQuiz()
    {
        SceneManager.LoadScene(0);
    }
}
