using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{


    public float damage;
    public float speed;
    public Vector3 direction;

    private Rigidbody rb;
    private bool onStart=true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("create a bullet");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(onStart){
            rb.velocity = direction * speed;
            Debug.Log($"current speed:{rb.velocity}");
            onStart=false;
        }
    }
}
