using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutSlidesData", menuName = "ScriptableObjects/Tut Slides Data", order = 1)]
public class TutorialSlidesData : ScriptableObject
{
    public Topic topic;
    public List<GameObject> tutorialSlides;
}
