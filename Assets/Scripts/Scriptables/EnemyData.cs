using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public Topic topic;
    public string enemyName;
    public Sprite enemySprite;
    public string answer;
    public Dialouge startingDialogue;
}
