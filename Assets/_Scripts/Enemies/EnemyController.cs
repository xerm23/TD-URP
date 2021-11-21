using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// script attached to each enemy prefab 
/// controls movement of enemy, taking/dealing damage
/// and manages tower in range
/// </summary>
public class EnemyController : MonoBehaviour
{
    public EnemyScriptable enemy;
    public List<TargetController> towersInRange = new List<TargetController>();
    public float currentHP;
    PathFollower pathFollower;
    float checkDistance = 0.1f;
    [SerializeField] Image healthBar;

    private void Start()
    {
        currentHP = enemy.maxHealth;
        pathFollower = GetComponent<PathFollower>();
        pathFollower.speed = enemy.moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            GameManager.singleton.AddGold(enemy.goldReward);
            RemoveFromTwrRangeAndDeactivate();
        }
    }

    private void Update()
    {
        healthBar.fillAmount = currentHP / enemy.maxHealth;
        healthBar.color = new Color(1, healthBar.fillAmount, healthBar.fillAmount);
        CheckIfReachedDest();
    }

    void CheckIfReachedDest()
    {
        if(pathFollower.FinishedMoving())
        {
            GameManager.singleton.RemovePlayerHP(enemy.damage);
            RemoveFromTwrRangeAndDeactivate(); 
        }
    }

    void RemoveFromTwrRangeAndDeactivate()
    {
        foreach (var item in towersInRange)
        {
            item.enemiesInRange.Remove(this);
            item.ResetTarget();
        }
        gameObject.SetActive(false);

    }

}
