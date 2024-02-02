using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIMapData", menuName = "ScriptableObjects/UI Map Data", order = 1)]
public class UIMapData : ScriptableObject
{
    public Topic topic;
    public GameObject mapPrefab;
    public Sprite mapSprite;
    public Sprite enemySprie;
    public string mapName;
    public int enemyCount;
}
