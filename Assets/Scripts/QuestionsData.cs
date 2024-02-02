using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "ScriptableObjects/Questions", order = 1)]
public class QuestionsData : ScriptableObject
{
    public Topic topic;
    public Questions questions;
}

[Serializable]
public class Questions
{
   
    public string question;
    public string answer;
    public string hint;
    [TextArea(2,10)][NonReorderable]
    public List<string> steps;
   

}
