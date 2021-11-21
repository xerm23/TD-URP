using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script used for controlling weapon of each tower
/// </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] public WeaponType typeSelected;
    [SerializeField] WeaponScriptable[] allWeapons; 
    WeaponScriptable currentWeapon;
    [HideInInspector]public EnemyController currentTarget;
    public List<BulletController> bullets = new List<BulletController>();

    public GameObject weaponGO;
    SphereCollider sphereCollider;
    Animator attackAnimator;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void ChangeTargetOfActiveBullets()
    {
        foreach (var item in bullets)
        {
            item.target = currentTarget;
        }
    }
    public void SetWeapon(WeaponType weaponNew)
    {
        sphereCollider = GetComponent<SphereCollider>();

        foreach (var weapon in allWeapons)
        {
            if (weapon.type == weaponNew)
            {
                currentWeapon = weapon;
                SpawnWeapon(0);
            }
        }
        sphereCollider.radius = currentWeapon.range/2;
    }

    public void SpawnWeapon(int towerLevel)
    {
        if (weaponGO != null)
            Destroy(weaponGO);

        GetComponent<BasicTower>().SetRadiusIndicator(currentWeapon.range);

        float pos = towerLevel * .5f + .22f;
        if (towerLevel == 3)
            pos = 1.55f;
        weaponGO = Instantiate(currentWeapon.weaponPrefab, transform.position + new Vector3(0, pos, 0), Quaternion.identity, transform);
        attackAnimator = weaponGO.GetComponent<Animator>();

        //assign to anim shooting 
        weaponGO.GetComponent<AnimShooting>().weaponController = this;
        weaponGO.GetComponent<AnimShooting>().currentWeapon = currentWeapon;
    }

    private void Update()
    {
        if (typeSelected != WeaponType.none && GetComponent<BasicTower>().planted)
            AttackControl();
    }

    void AttackControl()
    {
        //if weapon has target rotate it towards it
        if (currentTarget != null )
        {
            RotateWeaponTowardsTarget();

            //attack animation
            if (attackAnimator != null)
            {
                attackAnimator.SetBool("Attack", true);
            }
        }
        else
        {
            //reset the attack
            if (attackAnimator != null)
                attackAnimator.SetBool("Attack", false);
        }
    }

    //if we change weapon during the game.. unused now
    void WeaponTypeChange()
    {
        if(typeSelected != currentWeapon.type)
        {
            foreach (var weapon in allWeapons)
            {
                if (weapon.type == typeSelected)
                {
                    currentWeapon = weapon;
                    Destroy(weaponGO);
                    weaponGO = Instantiate(weapon.weaponPrefab, transform.position + new Vector3(0, 1.55f, 0), Quaternion.identity, transform);
                }
            }
            sphereCollider.radius = currentWeapon.range;
        }
    }
    private void OnDrawGizmos()
    {
        if(currentWeapon != null)
        {
            //draw range indication
            Gizmos.color = new Color(0, 1, 1, .2f);
            Gizmos.DrawSphere(transform.position, currentWeapon.range);
        }
    }

    float rotateSpeed = 5f;
    void RotateWeaponTowardsTarget()
    {
        var lookPos = currentTarget.transform.position - weaponGO.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        weaponGO.transform.rotation = Quaternion.Slerp(weaponGO.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
