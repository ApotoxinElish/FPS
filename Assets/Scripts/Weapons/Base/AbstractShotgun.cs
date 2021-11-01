using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractShotgun : AbstractGun
{
    public int projectile;

    public float maxScatteringRadian;
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

    Quaternion getScattering(float maxRadian){
        float radius=Random.Range(0,(float)System.Math.Tan(maxRadian));
        float radian=Random.Range(0,360);
        Vector3 randomPoint = new Vector3(1f,radius*(float)System.Math.Cos(radian),radius*(float)System.Math.Sin(radian));
        randomPoint.Normalize();

        Quaternion scattering=Quaternion.FromToRotation(new Vector3(1,0,0),randomPoint);

        return scattering;
    }

    public override void shoot(){
        for(int i=0;i<projectile;i++){
            Quaternion scattering=getScattering(maxScatteringRadian);
            GameObject currentBullet=Instantiate(bullet,transform.position,scattering*transform.rotation);
            PrototypeBullet prototypeBulletScript=currentBullet.GetComponent("PrototypeBullet") as PrototypeBullet;
            prototypeBulletScript.damage=bulletDamage;
            prototypeBulletScript.direction=(transform.rotation*scattering*Vector3.right);
            prototypeBulletScript.speed=bulletSpeed;
        }

    }
}
