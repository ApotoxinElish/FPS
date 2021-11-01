using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPistol : AbstractGun
{

    void Update()
    {
        coolDown+=Time.deltaTime;
        currentReloadTime+=Time.deltaTime;

        if(Input.GetButtonDown("Fire1")){
            Debug.Log("try to fire");

            if(reloading==true){
                Debug.Log("reloading");
                if(currentReloadTime>=reloadTime){
                    reloading=false;
                    Debug.Log("finish reloading");
                }
            }
            else
            {
                if(currentAmmunition<=0){
                    Debug.Log("start to reload");
                    reload();
                    reloading=true;
                    currentReloadTime=0;
                    currentAmmunition=ammunition;
                }

                else if(coolDown >= 1/fireRate){
                    shoot();
                    coolDown=0;
                    currentAmmunition--;
                    Debug.Log($"shoot {currentAmmunition} left");
                }
                else
                {
                    Debug.Log("cooldowning");
                }
            }
            
        }
    }
    public override void shoot(){
        GameObject currentBullet=Instantiate(bullet,transform.position,Quaternion.LookRotation(transform.rotation*Vector3.forward));
        PrototypeBullet prototypeBulletScript=currentBullet.GetComponent("PrototypeBullet") as PrototypeBullet;
        prototypeBulletScript.damage=bulletDamage;
        prototypeBulletScript.direction=transform.rotation*Vector3.right;
        prototypeBulletScript.speed=bulletSpeed;
    }
}
