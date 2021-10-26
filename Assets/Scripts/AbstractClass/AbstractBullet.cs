using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{


    public float damage;
    public float speed;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
