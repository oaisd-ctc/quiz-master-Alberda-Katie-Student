using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionSceen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }
    public int GetQUestionSceen()
    {
        return questionSceen;
    }
    public void IncrementQuestionSeen()
    {
        questionSceen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers/ (float)questionSceen * 100);
    }




}
