using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "ScriptableObjects/Bullet")]
public class BulletScriptable : ScriptableObject
{
    [Header("Stats")]
    [Space(40)]
    public float speed;
    public float damage;

    //later could be possible to add something like bullet spells (slow/stun target... etc)
    //bullet spells could be invoked on target hit 
}
