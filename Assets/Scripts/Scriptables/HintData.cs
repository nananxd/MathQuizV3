using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HintData", menuName = "ScriptableObjects/Hint", order = 1)]
public class HintData : ScriptableObject
{
    public Topic topic;
    [NonReorderable]
    public List<Tutorial> hints;
}
