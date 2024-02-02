using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/Question Data", order = 1)]
public class QuestionScriptable : ScriptableObject
{
    public Topic topic;
    public Difficulty difficulty;
    public string question;
    public string correctAnswer;
    public List<string> incompleteAnswers;
    [Header("Correct answers")]
    public List<string> missingAnswer;

    [NonReorderable]
    public List<StepByStep> questionsStepsToSolve;
}

[System.Serializable]
public class StepByStep
{
    public string explanation;
    //public string solution;
}
