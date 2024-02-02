using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StepsData", menuName = "ScriptableObjects/Steps", order = 1)]
public class StepsData : ScriptableObject
{
    public Topic topic;
    [NonReorderable]
    public List<Tutorial> steps;
}
