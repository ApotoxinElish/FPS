using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypePistol : AbstractGun
{

    public override void shoot(){
        PrototypeBullet prototypeBulletScript=bullet.GetComponent("PrototypeBullet") as PrototypeBullet;
        prototypeBulletScript.damage=bulletDamage;
        prototypeBulletScript.direction=transform.rotation*Vector3.left;
        prototypeBulletScript.speed=bulletSpeed;
        Instantiate(bullet,transform.position,Quaternion.LookRotation(transform.rotation*Vector3.left));
    }
}
