using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// basic weapon scriptable
/// </summary>
public enum WeaponType
{
    none, ballista, blaster, cannon, catapult
};
[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [Header("Object type")]
    [Space(40)]
    public WeaponType type;
    public GameObject weaponPrefab;

    [Header("Stats")]
    [Space(40)]
    public float range;
    public float attackSpeed;
}
