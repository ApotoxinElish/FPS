using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Characters.MovingController;
using UnityEngine;

public class HealthBottle : AbstractBottle
{
    // Start is called before the first frame update
    public int health;
    public float time;
    private string type = "Hp";
    void Start()
    {
        SetValue(health,time,type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ValueUp(GameObject other)
    {
        if (other.layer ==7)
        {
            var playerMovingScript= other.GetComponent<Player>();
            playerMovingScript.Heal(health);
            //other.GetComponent<Player>().Heal(health);
        }
        DestoryBottle();
    }

    public void OnCollisionEnter(Collision other)
    {   
        ValueUp(other.gameObject);
    }
}
