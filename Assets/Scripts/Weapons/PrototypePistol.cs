using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypePistol : AbstractGun
{

    public override void shoot(){
        PrototypeBullet prototypeBulletScript=bullet.GetComponent("PrototypeBullet") as PrototypeBullet;
        prototypeBulletScript.damage=10;
        prototypeBulletScript.direction=Vector3.forward;
        prototypeBulletScript.speed=1;
        Instantiate(bullet,transform.position,transform.rotation);
    }
}
