using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework.Managers;

public class WeaponHolder : MonoBehaviour
{
    public GameObject oriWeapon;

    private GameObject weapon;
    AbstractGun weaponScript;
    // Start is called before the first frame update
    void Start()
    {
        string weaponName= oriWeapon.name.Split('(')[0];
        Debug.Log($"weaponname {weaponName}");
        weapon=Instantiate(oriWeapon,transform.position,transform.rotation,transform);
        weaponScript=weapon.GetComponent(typeof(AbstractGun)) as AbstractGun;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 r= Quaternion.FromToRotation(Vector3.forward,AimCrossHairManager.Instance.targetPoint-transform.position).eulerAngles;
        transform.rotation=Quaternion.Euler(r.x,r.y,0);
        


        if(weaponScript.inputReload()){
            weaponScript.reload();
        }

        if(weaponScript.inputActivate()){
            weaponScript.Activate();
        }
    }

    void switchWeapon (GameObject newWeapon)
    {
        
        return;
    }
}
