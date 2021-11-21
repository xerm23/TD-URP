using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script attached to each tower prefab
/// </summary>
public class BasicTower : MonoBehaviour
{
    WeaponController weaponCtrl;

    //tower parts used for the upgrade system which is not yet implemented.
    [Header("Tower Parts")]
    [SerializeField] protected GameObject[] bottomParts; 
    [SerializeField] protected GameObject[] midParts;
    [SerializeField] protected GameObject[] roofParts;

    [Header("Active part")]
    protected int bottomPart = 0;
    protected int midPart = 0;
    protected int roofPart = 0;
    protected int weaponPart = -1;

    protected GameObject baseGO;
    protected GameObject bottomPartGO;
    protected GameObject midPartGO;
    protected GameObject roofPartGO;


    public int towerLevel = 0;
    public bool planted = false;
    [SerializeField] GameObject radiusIndicator;
    [SerializeField] GameObject canvasConfirm;


    public void TowerPlanted()
    {
        planted = true;
        radiusIndicator.SetActive(false);
    }

    private void Start()
    {
        bottomPart = Random.Range(0, bottomParts.Length);
        midPart = Random.Range(0, midParts.Length);
        roofPart = Random.Range(0, roofParts.Length);

        weaponCtrl = GetComponent<WeaponController>();
    }

    public void SetRadiusIndicator(float radius)
    {
        radiusIndicator.transform.localScale = Vector3.one * radius;
    }

    #region NOT USED RIGHT NOW

    void UpgradeTower()
    {
        towerLevel++;
        if(towerLevel == 1)
        {
            bottomPartGO = Instantiate(bottomParts[bottomPart], transform);
            bottomPartGO.transform.localPosition = new Vector3(0, .25f, 0);
        }
        if (towerLevel == 2)
        {
            midPartGO = Instantiate(midParts[midPart], transform);
            midPartGO.transform.localPosition = new Vector3(0, .75f, 0);
        }
        if (towerLevel == 3)
        {
            roofPartGO = Instantiate(roofParts[roofPart], transform);
            roofPartGO.transform.localPosition = new Vector3(0, 1.25f, 0);
        }
        weaponCtrl.SpawnWeapon(towerLevel);
    }

    void ChangeThePart()
    {
        //change bot
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bottomPart++;
            if (bottomPart >= bottomParts.Length)
                bottomPart = 0;

            bottomPartGO.GetComponent<MeshFilter>().mesh = bottomParts[bottomPart].GetComponent<MeshFilter>().sharedMesh;
        }
        //change mid
        if (Input.GetKeyDown(KeyCode.X))
        {
            midPart++;
            if (midPart >= midParts.Length)
                midPart = 0;

            midPartGO.GetComponent<MeshFilter>().mesh = midParts[midPart].GetComponent<MeshFilter>().sharedMesh;
        }
        //change roof
        if (Input.GetKeyDown(KeyCode.C))
        {
            roofPart++;
            if (roofPart >= roofParts.Length)
                roofPart = 0;

            roofPartGO.GetComponent<MeshFilter>().mesh = roofParts[roofPart].GetComponent<MeshFilter>().sharedMesh;
        }

    }
    #endregion

}
