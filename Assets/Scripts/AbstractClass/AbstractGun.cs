using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGun : MonoBehaviour
{
    public int ammunition;
    public float fireRate;
    public float reloadTime;
    public GameObject bullet;

    private int currentAmmunition;
    private float coolDown;
    private bool reloading;
    private float currentReloadTime;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmunition=ammunition;
        reloading=false;
        coolDown=float.MaxValue;
        currentReloadTime=float.MaxValue;
    }

    // Update is called once per frame
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

        if(Input.GetButtonDown("Jump")){
            Debug.Log("try to jump");
        }
    }

    public virtual void reload(){
    }
    public virtual void shoot(){
    }

}
