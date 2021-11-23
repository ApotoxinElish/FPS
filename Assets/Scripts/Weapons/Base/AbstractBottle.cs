using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Character;

public abstract class AbstractBottle : MonoBehaviour
{
    public Player player;
    private int _value; // the value of the bottle
    private float _existTime; // the exist period of the bottle
    private string _type;
    
    protected virtual void SetValue(int initValue, float initTime, string type)  // init hp values
    {
        _value = initValue;
        _existTime = initTime;
        _type = type;
    }

    public virtual void ValueUp(GameObject other)
    {
        Debug.Log("value up"); // call the different user function.
    }
    public void DestoryBottle()
    {
        Destroy(this.gameObject);
    }

    public void FadeAnimator()
    {
        // the fade animation for the bottle
    }
}
