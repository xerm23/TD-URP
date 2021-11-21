using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptable : ScriptableObject
{
    [Header("Stats")]
    [Space(40)]
    public float moveSpeed;
    public float maxHealth;
    public int damage;
    public int goldReward;
    public float spawnInterval;
    public Sprite waveSprite;
}
