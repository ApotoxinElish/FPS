using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractShotgun : AbstractGun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Vector3 getScattering(float maxRadian){
        //float radius=Random.Range(0,(float)System.Math.Tan(maxRadian));
        //float radian=Random.Range(0,360);
        //Vector3 randomPoint=new Vector3(1,radius*(float)System.Math.Cos(radian),radius*(float)System.Math.Sin(radian)).Normalize();
        //Vector3 scattering=Vector3.RotateTowards(new Vector3(1,0,0),randomPoint,(float)System.Math.PI*0.5,0);
        
        //return scattering;
    //}
}
