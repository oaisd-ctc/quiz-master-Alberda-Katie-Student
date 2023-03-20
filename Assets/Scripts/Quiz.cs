using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [Header("Answer")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly;
    int correctAnswerIndex;
    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Update() {
        {
            timerImage.fillAmount = timer.fillFraction;
            if(timer.loadNextQuestion)
            {
                hasAnsweredEarly = false;
                GetNextQuestion();
                timer.loadNextQuestion = false;
            }
            else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
            {
                DisplayAnswer(-1);
                SetButtonState(false);

            }
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + " %";
    }
    void DisplayAnswer(int index)
    {
         Image buttonImage;
        if(index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            scoreKeeper.IncrementCorrectAnswers();
            

        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry the corret answer was " + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

    }
    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
        scoreKeeper.IncrementQuestionSeen();
    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i=0; i< answerButtons.Length; i++)
        {
             TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }

    }
    void SetButtonState(bool state)
    {
        for (int i =0; i<answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprite()
    {
        for (int i = 0; i< answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }

}
