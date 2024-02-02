using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/Tutorial", order = 1)]
public class TutorialData : ScriptableObject
{
    public Topic topic;
    [NonReorderable]
    public List<Tutorial> tutorialData;
    
}

[Serializable]
public class Tutorial
{
    //public Dialouge dialogues;
    //public Sprite example;
    [TextArea(3,5)]
    public string explanation;

    [TextArea(3, 5)]
    public string example;
    public Sprite sprite;//optional
}
