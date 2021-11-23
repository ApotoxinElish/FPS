using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int ID;
    public bool Finished;

    private Transform doorsTran;

    // Start is called before the first frame update
    void Start()
    {
        doorsTran=transform.Find("Collisions").Find("Doors");
    }

    // Update is called once per frame
    void Update()
    {
        Finished=checkFinished();
    }

    private bool checkFinished(){
        return transform.Find("Enemies").childCount==0;
    }

    public void clearEnemy(){

        Transform enemiesTransform=this.gameObject.transform.Find("Enemies");

        for(int i=0; i<enemiesTransform.gameObject.transform.childCount; i++){
            GameObject enemy=enemiesTransform.GetChild(i).gameObject;
            Destroy(enemy);
        }
    }

    public void openDoors(){
        
        for(int i=0; i<doorsTran.childCount; i++){
            doorsTran.GetChild(i).GetComponent<BoxCollider>().isTrigger=true;
            Debug.Log("RoomManager: open door");
        }
    }

    public void closeDoors(){
         for(int i=0; i<doorsTran.childCount; i++){
            doorsTran.GetChild(i).GetComponent<BoxCollider>().isTrigger=false;
            Debug.Log("RoomManager: close door");
        }
    }
}
