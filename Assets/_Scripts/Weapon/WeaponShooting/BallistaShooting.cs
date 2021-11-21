using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaShooting : AnimShooting
{
    public GameObject arrowReference;

    public void Shoot()
    {
        GameObject newBullet = Instantiate(arrowReference, this.transform, true);
        newBullet.SetActive(true);
        newBullet.GetComponent<BulletController>().target = weaponController.currentTarget;
        newBullet.GetComponent<BulletController>().weaponController = weaponController;
        weaponController.bullets.Add(newBullet.GetComponent<BulletController>());
    }

}
