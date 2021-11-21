using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script used for flying bullet towards target
/// </summary>

public class BulletController : MonoBehaviour
{
    [SerializeField] BulletScriptable bullet;
    public EnemyController target;
    public WeaponController weaponController;

    float distanceOfExplosion = .2f;
    private void Update()
    {
        //target dies while bullet is flying
        if (target == null)
        {
            weaponController.bullets.Remove(this);
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * bullet.speed);

            LerpLookAt();
            if (Vector3.Distance(transform.position, target.transform.position) < distanceOfExplosion)
            {
                //hit the target!!
                DoTheDamage();
            }
        }
    }
    float lerpSpeed = 500f;

    void LerpLookAt()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion toRotation;
        if (direction != Vector3.zero)
            toRotation = Quaternion.LookRotation(direction);
        else toRotation = Quaternion.identity;
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, lerpSpeed * Time.time);
    }

    void DoTheDamage()
    {
        weaponController.bullets.Remove(this);
        target.TakeDamage(bullet.damage);
        //do the animation of explosion here
        //but for now just die
        Destroy(gameObject);
    }




}
