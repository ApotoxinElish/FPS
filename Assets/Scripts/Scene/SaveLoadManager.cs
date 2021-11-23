using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using Character;
using UIFramework.Managers;
public class SaveLoadManager : MonoBehaviour
{
    public Transform roomsTran;

    public Transform UITran;
    public GameObject player;
    public string fileName = "Assets/Datas/save.json";

    public void save()
    {

        SaveData saveData = new SaveData();
        LevelData levelData = new LevelData();
        PlayerData playerData = new PlayerData();


        levelData.levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int[] finishedRoomIDs = getFinishedRoomID();
        levelData.finishedRoomIDs = finishedRoomIDs;

        Player playerScript = player.GetComponent(typeof(Player)) as Player;
        playerData.hp = playerScript.GetHp();
        playerData.position = player.transform.position;
        WeaponHolder wphdrScript = playerScript.weaponHolder.GetComponent(typeof(WeaponHolder)) as WeaponHolder;
        playerData.weaponName = wphdrScript.weapon.name.Split('(')[0];

        saveData.levelData = levelData;
        saveData.playerData = playerData;


        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText(fileName, saveDataJson);
        Debug.Log(saveDataJson);
    }

    private void load()
    {

        string saveDataJson = File.ReadAllText(fileName);
        SaveData saveData = new SaveData();
        JsonUtility.FromJsonOverwrite(saveDataJson, saveData);

        for (int i = 0; i < 10; i++)
        {
            int id = saveData.levelData.finishedRoomIDs[i];
            if (id >= 0)
            {
                RoomManager roomManager = roomsTran.GetChild(i).gameObject.GetComponent(typeof(RoomManager)) as RoomManager;
                roomManager.clearEnemy();
            }
        }

        GameObject player = Instantiate(Resources.Load("Prefabs/Characters/Hero0"), saveData.playerData.position, Quaternion.identity) as GameObject;
        Player playerScript = player.GetComponent(typeof(Player)) as Player;

        if(saveData.playerData.hp>0){
            playerScript.SetHp(saveData.playerData.hp);
        }


        WeaponHolder wphdrScript = playerScript.weaponHolder.GetComponent(typeof(WeaponHolder)) as WeaponHolder;
        GameObject weapon=Resources.Load($"Prefabs/Weapons/Guns/{saveData.playerData.weaponName}") as GameObject;
        Debug.Log(weapon.name);
        wphdrScript.oriWeapon = weapon;

        AimCrossHairManager ACHMScript = UITran.Find("Canvas").Find("aim").GetComponent(typeof(AimCrossHairManager)) as AimCrossHairManager;
        ACHMScript.virtualCamera = player.transform.Find("CM");

    }


    private void Start()
    {
        load();
    }

    public static void generateDefultJson()
    {

        SaveData saveData = new SaveData();
        LevelData levelData = new LevelData();
        PlayerData playerData = new PlayerData();

        levelData.levelName = "Level1";

        int[] finishedRoomID = new int[10];
        for (int i = 0; i < 10; i++) { finishedRoomID[i] = -1; }
        levelData.finishedRoomIDs = finishedRoomID;

        playerData.hp = -1;
        playerData.position = new Vector3(0, 1, 0);
        playerData.weaponName = "PrototypePistol";

        saveData.levelData = levelData;
        saveData.playerData = playerData;

        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText("Assets/Datas/save.json", saveDataJson);
        Debug.Log($"SaveDefult: {saveDataJson}");


        return;
    }

    public void switchSave()
    {
        return;
    }

    public static string getLevelName()
    {
        return "";
    }






    private int[] getFinishedRoomID()
    {

        int[] finishedRoomID = new int[10];
        for (int i = 0; i < 10; i++) { finishedRoomID[i] = -1; }

        int finishedRoomNum = 0;

        for (int i = 0; i < roomsTran.gameObject.transform.childCount; i++)
        {
            RoomManager roomManager = roomsTran.GetChild(i).gameObject.GetComponent(typeof(RoomManager)) as RoomManager;
            if (roomManager.Finished)
            {
                finishedRoomID[finishedRoomNum++] = i;
            }
        }
        return finishedRoomID;
    }
}



[Serializable]
public class SaveData
{
    public LevelData levelData;
    public PlayerData playerData;
}

[Serializable]
public class LevelData
{
    public string levelName;
    public int[] finishedRoomIDs;
}

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public int hp;
    public string weaponName;

}

