using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{


    public float damage;
    public float speed;
    public Vector3 direction;
    private float destorytime = 10.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("bullet: create a bullet");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
        Console.WriteLine($"current speed:{rb.velocity.ToString()}");
        Invoke("DestoryBullet",destorytime);
    }

    private void OnTriggerEnter(Collider other) {
            Destroy(this.gameObject);
            
    }

    private void DestoryBullet()
    {
        Destroy(this);
        Debug.Log("bullet: destory");
    }

}
