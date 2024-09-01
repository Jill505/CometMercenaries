using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneShotTester : MonoBehaviour
{
    public GameCore gCore;
    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {
        //warningFunc();
    }

    public void warningFunc()
    {
        Debug.LogWarning("One Shot Tester Functioned");
    }

    public void cleanSaveData()
    {

    }
    public void cleanMercenariesList()
    {
        Debug.Log("Clean Mercenaries List Already");
        GameCore.Camp.MercenariesList.Clear();
        GameCore.Save();
    }

    public void cleanAllNodes()
    {

    }

    public void GiveExp4()
    {
        GameObject.Find("GameCore").GetComponent<EditSystem>().mercenariesLoading.GainExp(4);
    }

    public void healthAllRecover()
    {
        GameObject.Find("GameCore").GetComponent<EditSystem>().mercenariesLoading.health = GameObject.Find("GameCore").GetComponent<EditSystem>().mercenariesLoading.maxHealth;
    }

    public InputField weaponGetInputField;
    public void getNewWeapon()
    {
        int weapNum;
        string weapStr = weaponGetInputField.text;
        if (int.TryParse(weapStr,out weapNum))
        {
            weapon newWeapon = gCore.defultWeaponStore.GetComponent<SystemDefultWeaponHouse>().defultWeapons[weapNum];

            GameCore.Camp.weaponStorehouseList.Add(newWeapon);
            GameCore.Save();
            weaponGetInputField.text = "";

            Debug.Log("已加入新武器至倉庫");
            Debug.Log(JsonUtility.ToJson(newWeapon));
        }
        else
        {
            Debug.Log("你給我看這三小");
        }

    }

    public weapon editingWeapon;
    public void weaponOpen()
    {
        int weapNum;
        string weapStr = weaponGetInputField.text;
        if (int.TryParse(weapStr, out weapNum))
        {
            editingWeapon = GameCore.Camp.weaponStorehouseList[weapNum];
        }
        else
        {
            Debug.Log("你給我看這三小");
        }
    }
    public void endEditingWeapon()
    {
        int weapNum;
        string weapStr = weaponGetInputField.text;
        if (int.TryParse(weapStr, out weapNum))
        {
            GameCore.Camp.weaponStorehouseList[weapNum] = editingWeapon;
            GameCore.Save();

            weaponGetInputField.text = "";

            Debug.Log("成功編輯武器");
        }
        else
        {
            Debug.Log("你給我看這三小");
        }
    }

    public void resetTheNodeSystem()
    {
        GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().nodeGiveInitialization();
        GameCore.Camp.worldMapNodeList = GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().defultNodes;

        GameCore.Save();
    }
}
