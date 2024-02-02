using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ChoicesData", menuName = "ScriptableObjects/Choices", order = 1)]
public class ChoicesData : ScriptableObject
{
   

    public List<Choices> choices;
}

[Serializable]
public class Choices
{
    public string name;
    public string choicesValue;
}
