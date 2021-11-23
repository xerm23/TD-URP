using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShooting : AnimShooting
{
    public GameObject[] bulletRef;
    public void AnimShoot0()
    {
        GameObject newBullet = Instantiate(bulletRef[0], this.transform, true);
        newBullet.SetActive(true);
        newBullet.GetComponent<BulletController>().target = weaponController.currentTarget;
        newBullet.GetComponent<BulletController>().weaponController = weaponController;
        weaponController.bullets.Add(newBullet.GetComponent<BulletController>());
    }
    public void AnimShoot1()
    {
        GameObject newBullet = Instantiate(bulletRef[1], this.transform, true);
        newBullet.SetActive(true);
        newBullet.GetComponent<BulletController>().target = weaponController.currentTarget;
        newBullet.GetComponent<BulletController>().weaponController = weaponController;
        weaponController.bullets.Add(newBullet.GetComponent<BulletController>());
    }
}
