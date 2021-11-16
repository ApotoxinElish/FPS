using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGun : MonoBehaviour
{
    public int ammunition;
    public float fireRate;
    public float reloadTime;
    public GameObject bullet;

    public float bulletDamage;
    public float bulletSpeed;

    protected int currentAmmunition;
    protected float coolDown;
    protected bool reloading;
    protected float currentReloadTime;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmunition=ammunition;
        reloading=false;
        coolDown=float.MaxValue;
        currentReloadTime=float.MaxValue;
    }

    void Update() {
        coolDown+=Time.deltaTime;
        currentReloadTime+=Time.deltaTime;
    }


    public virtual void shoot(){
        coolDown=0;
        currentAmmunition--;
        GameObject currentBullet=Instantiate(bullet,transform.position,Quaternion.LookRotation(transform.rotation*Vector3.forward));
        PrototypeBullet prototypeBulletScript=currentBullet.GetComponent("PrototypeBullet") as PrototypeBullet;
        prototypeBulletScript.damage=bulletDamage;
        prototypeBulletScript.direction=transform.rotation*Vector3.forward;
        prototypeBulletScript.speed=bulletSpeed;
    }
    public virtual void reload(){
        reloading=true;
        currentReloadTime=0;
    }
    public virtual bool inputActivate(){
        return Input.GetButtonDown("Fire1");
    }

    public virtual bool inputReload(){
        return false;
    }

    public virtual void Activate(){

        Debug.Log("activate");



        if(reloading==true){
            Debug.Log("reloading");
            if(currentReloadTime>=reloadTime){
                reloading=false;
                Debug.Log("finish reloading");
                currentAmmunition=ammunition;
            }
        }
        else
        {

            if(currentAmmunition<=0){
                Debug.Log("start to reload");
                reload();
            }
            else if(coolDown >= 1/fireRate){
                Debug.Log($"shoot {currentAmmunition-1} left");
                shoot();
            }
            else
            {
                Debug.Log("cooldowning");
            }
        }
    }

}
