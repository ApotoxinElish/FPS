using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using Characters.MovingController;

public class HpUpBottle : AbstractBottle
{
    // Start is called before the first frame update
    public int HpUp;
    public float time;
    private string type = "HpUp";
    void Start()
    {
        SetValue(HpUp,time,type);
    }
    

    public override void ValueUp(GameObject other)
    {
        if (other.layer ==7)
        {
            var playerMovingScript= other.GetComponent<Player>();
            playerMovingScript.ExtendHpMaxByValue(HpUp);
            //other.GetComponent<Player>().Heal(health);
        }
        DestoryBottle();
    }

    public void OnCollisionEnter(Collision other)
    {   
        ValueUp(other.gameObject);
    }
}
