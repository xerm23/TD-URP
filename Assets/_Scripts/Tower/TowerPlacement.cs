using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script used for placing the tower
/// </summary>
public class TowerPlacement : MonoBehaviour
{
    public GameObject towersGroup;
    public GameObject mainButton;

    [SerializeField] GameObject towerPrefab;
    [SerializeField] GameObject background;
    [SerializeField] GameObject towerPlacement;
    [SerializeField] Transform towersParent;
    [SerializeField] RectTransform confirmPanel;
    public WeaponType towerSelected = WeaponType.none;
    int towerPrice;
    [SerializeField] bool firstTower = false;

    //used by main twr button to enable tower selection panel 
    public void clickTowerBtnMain()
    {
        LeanTween.scale(mainButton, Vector3.zero, .2f).setOnComplete(OpenTowersTab);
    }

    //activate animation of main button for tower placement
    void ActivateMainButton()
    {
        mainButton.transform.localScale = Vector3.zero;
        mainButton.SetActive(true);
        LeanTween.scale(mainButton, Vector3.one, .2f);
    }

    //activate animation for tower selection panel
    void OpenTowersTab()
    {
        mainButton.SetActive(false);
        towersGroup.transform.localScale = Vector3.zero;
        towersGroup.SetActive(true);
        LeanTween.scale(towersGroup, Vector3.one, .2f);
    }

    //x button clicked
    public void clickCloseTowers()
    {
        LeanTween.scale(towersGroup, Vector3.zero, .2f).setOnComplete(CloseTowersTab).setOnComplete(ActivateMainButton);
    }

    //close towers panel group animation
    void CloseTowersTab()
    {
        towersGroup.SetActive(false);
        towersGroup.SetActive(true);
        mainButton.transform.localScale = Vector3.zero;
    }

    //accesed by onclick events from tower selected panel buttons
    public void towerSelection(int type)
    {
        switch (type)
        {
            case 1:
                towerPrice = 150;
                break;
            case 2:
                towerPrice = 100;
                break;
            case 3:
                towerPrice = 200;
                break;
            case 4:
                towerPrice = 250;
                break;
        }
        if (GameManager.singleton.currentGold < towerPrice)
            return;
        towerSelected = (WeaponType) type;
        towerPlacement.GetComponent<WeaponController>().SetWeapon(towerSelected);
        LeanTween.scale(towersGroup, Vector3.zero, .2f).setOnComplete(CloseTowersTab);
    }

    private void Update()
    {
        ShowTowerPosition();
    }
    
    //shows preview of tower when tower selected is none
    void ShowTowerPosition()
    {
        if (towerSelected == WeaponType.none)
            return;

        towerPlacement.GetComponent<WeaponController>().typeSelected = towerSelected;
        towerPlacement.SetActive(true);
        background.SetActive(false);
        towerPlacement.transform.position = towerPlantingPos;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlaceTower(false);
        }
    }


    Vector3 towerPlantingPos;
    GameObject tileHit;

    void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //towers can be placed only on layer 7 -> basic tiles
        int layermask = 1 << 7;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            if (towerSelected != WeaponType.none && !confirmPanel.gameObject.activeInHierarchy)
            {
                towerPlantingPos = hit.transform.position + new Vector3(0, .2f, 0);
                if (Input.GetMouseButtonDown(0))
                {
                    tileHit = hit.transform.gameObject;
                    ActivateConfirmPanel();
                }
            }
        }
    }

    public void ActivateConfirmPanel()
    {
        confirmPanel.gameObject.SetActive(true);
    }

    //accesed by onclick events from buttons in confirm panel
    public void PlaceTower(bool yes)
    {
        if (yes)
        {
            tileHit.layer = 1; // set tile hit layer to default, so it won't be able to spawn tower on it anymore

            GameObject towerGO = Instantiate(towerPrefab, towerPlacement.transform.position, Quaternion.identity, towersParent.transform);
            towerGO.GetComponent<BasicTower>().TowerPlanted();
            towerGO.GetComponent<WeaponController>().SetWeapon(towerSelected);
            GameManager.singleton.currentGold -= towerPrice;
            GameManager.singleton.ShowGold();
            
        }
        //reset panels
        towerSelected = WeaponType.none;
        towerPlacement.SetActive(false);
        background.SetActive(true);
        confirmPanel.gameObject.SetActive(false);
        ActivateMainButton();
    }


}
