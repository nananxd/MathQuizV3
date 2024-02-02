using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TopicUiData", menuName = "ScriptableObjects/Topic Ui Data", order = 1)]
public class TopicUIData : ScriptableObject
{
    public Topic topic;
    public string title;
    [TextArea(3, 10)]
    public string description;

    public Sprite backgroundImage;//hover state
    public Sprite normalStateBackground;
    public Sprite lockBackgroundImage;
    public Sprite lockIcon;

    public int scoreRequirement;
    public bool isLock;
}
