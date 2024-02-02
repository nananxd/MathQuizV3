using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Player Starting Stats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int health;
    public int damage;
    public GameObject modelPrefab;
}
