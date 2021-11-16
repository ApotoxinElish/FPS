using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : AbstractBullet
{
    void Update()
    {
        
    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
