using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ObjectCreateManager : MonoBehaviour
{
    /// <summary>
    /// this manager aims to create a manager for the stuff that player can pick up (health bottle, attack bottle etc)
    /// </summary>
    // Start is called before the first frame update
    public GameObject[] RandomObjects;

    public GameObject Field;
    private Vector3 FieldColliderCenter;
    private Vector3 FieldColliderSize;
    
    void Start()
    {
        FieldColliderCenter = Field.GetComponent<BoxCollider>().center;
        FieldColliderSize = Vector3.Max(Field.GetComponent<BoxCollider>().size,new Vector3(60,0,60));
        
        
        for (int i = 0; i < RandomObjects.Length; i++)
        {
            int x = Random.Range((int) (FieldColliderCenter.x - FieldColliderSize.x),
                (int) (FieldColliderCenter.x + FieldColliderSize.x));
            int z = Random.Range((int) (FieldColliderCenter.z - FieldColliderSize.z),
                (int) (FieldColliderCenter.z + FieldColliderSize.z));
            Vector3 spawnPos = new Vector3(x, 0, z);
            GameObject choosedObject = RandomObjects[Random.Range(0, RandomObjects.Length - 1)];
            GameObject spawnedObject = Instantiate(choosedObject, spawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
