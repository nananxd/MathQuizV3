using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObjects/Quest Data", order = 1)]
public class QuestData : ScriptableObject
{
    public Topic topic;
    public QuestType questType;
    public string questTitle;
    public string questDetail;
    public int questRequiredKill;
}

public enum QuestType
{
    Kill,
    Item
}