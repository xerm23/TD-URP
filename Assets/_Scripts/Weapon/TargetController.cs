using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script used to control when enemies enter in range of tower
/// </summary>
[RequireComponent (typeof(WeaponController))]
public class TargetController : MonoBehaviour
{
    WeaponController weaponController;

    public List<EnemyController> enemiesInRange;

    private void Start()
    {
        weaponController = GetComponent<WeaponController>();
        enemiesInRange.Clear();
    }

    public void ResetTarget()
    {
        if (enemiesInRange.Count > 0)
            weaponController.currentTarget = enemiesInRange[0];
        else weaponController.currentTarget = null;

        weaponController.ChangeTargetOfActiveBullets();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            enemiesInRange.Add(other.GetComponent<EnemyController>());
            other.GetComponent<EnemyController>().towersInRange.Add(this); //add reference of this tower to the enemy controller

            //set this enemy as target if tower has no target atm
            if(weaponController.currentTarget == null)
                weaponController.currentTarget = other.GetComponent<EnemyController>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
            if(weaponController.currentTarget == enemy)
            {
                ResetTarget();
            }
        }
    }

}
