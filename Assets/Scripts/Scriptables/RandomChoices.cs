using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomChoicesData", menuName = "ScriptableObjects/Random Choices Data", order = 1)]
public class RandomChoices : ScriptableObject
{
    public List<string> choices;
}
