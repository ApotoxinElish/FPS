using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour
{


    public float damage;
    public float speed;
    public Vector3 direction;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("create a bullet");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
        Debug.Log($"current speed:{rb.velocity}");
    }

    private void OnTriggerEnter(Collider other) {
            Destroy(this);
    }




}
