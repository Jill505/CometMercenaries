using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UIElements.Experimental;
using System.Linq;

public class GameCore : MonoBehaviour
{
    static public string campSyssSavePath;

    static public CampSystem Camp = new CampSystem();
    static public SaveSystem saveSystem = new SaveSystem();

    private void Awake()
    {
        //初始化
        campSyssSavePath = Application.persistentDataPath + "/saveFiles.json";

        Camp = new CampSystem();
        Camp.worldMapNodeList = new List<Node>();
        Camp.MercenariesList = new List<Mercenaries>();
        Camp.tempWorldMapNodeList = Camp.worldMapNodeList.ToArray();
        Camp.tempMercenariesList = Camp.MercenariesList.ToArray();

        Debug.Log("完成初始化");

        int testNum = GameCore.Camp.MercenariesList.Count;
        Debug.Log(testNum);


        Load();
    }

    private void Start()
    {
    }

    private void Update()
    {
        string swapEdit = "克朗："+ Camp.Kroan +"\n紀念幣："+ Camp.C_Coin + "\n聲望："+Camp.popularity + "\n";
        testCodeInfo.text = swapEdit; 
    }

    static public void Save()
    {
        saveSystem.savePlayerFile();
    }
    static public void Load()
    {
        saveSystem.loadPlayerFile();
    }

    //以下為測試用代碼 不用於正式遊戲
    public Text testCodeInfo;
    public void testCodeGainKroan()
    {
        Camp.Kroan += 30f;
        Save();
    }
}


[System.Serializable]
public class Mercenaries
{
    public string characterName = "Defult Name";
    //角色基礎屬性
    public float level = 0;
    public float MaxLevel = 40;
    public float exp = 0;
    public float nextLevelRequireExp = 10;

    public float health = 10;
    public float maxHealth = 10;

    //角色裝備
    public Gear HeadGear;//頭部裝備
    public Gear ChestGear;//胸部裝備
    public Gear FootGear;//腳部裝備
    public Gear tokenGear;//信物


    //角色職業技能
    public float characterPropotyPoints = 0;
    public float characterSkillPoints = 2; //初始擁有2

    //角色四大屬性
    /// <summary>
    /// 血肉 體能 (進戰攻擊 - 血量 - 負重)
    /// 靈魂 智慧 (乙太攻擊 - 智慧相關屬性)
    /// 順熵 敏捷 (玩家速度 - 火槍傷害 - 暴擊傷害)
    /// 業力 精神 (補血量 - 醫院工作效率)
    /// </summary>
    public float characterStrength = 0;
    public float characterEther = 0;
    public float characterAgility = 0;
    public float characterMentle = 0;

    //遊戲內參數 
    public float violentEnergyRadius = 2;
    public float gameSpeed = 0;//遊戲內速度 用於計算
    public float violentEnergyPointws = 0;

}

[System.Serializable]
public class SaveSystem
{
    public void savePlayerFile()
    {
        GameCore.Camp.syncTempData_ForSave();

        CampSystem tempCamp = new CampSystem();

        tempCamp.C_Coin = GameCore.Camp.C_Coin;
        tempCamp.Kroan = GameCore.Camp.Kroan;
        tempCamp.popularity = GameCore.Camp.popularity;

        tempCamp.tempMercenariesList = GameCore.Camp.tempMercenariesList;
        tempCamp.tempWorldMapNodeList = GameCore.Camp.tempWorldMapNodeList;

        string saveFiles = JsonUtility.ToJson(tempCamp);
        File.WriteAllText(GameCore.campSyssSavePath,saveFiles);

        Debug.Log("SavingPlayerFiles");
    }
    public void loadPlayerFile()
    {
        Debug.Log("LoadingPlayerFiles");
        if (File.Exists(GameCore.campSyssSavePath))
        {
            //load
            CampSystem swapCamp = JsonUtility.FromJson<CampSystem>(File.ReadAllText(GameCore.campSyssSavePath));

            GameCore.Camp.C_Coin = swapCamp.C_Coin;
            GameCore.Camp.Kroan = swapCamp.Kroan;
            GameCore.Camp.popularity = swapCamp.popularity;
            Debug.Log("克朗數"+swapCamp.Kroan);

            if (swapCamp.tempMercenariesList != null)
            {
                GameCore.Camp.tempMercenariesList = swapCamp.tempMercenariesList;
                GameCore.Camp.MercenariesList = GameCore.Camp.tempMercenariesList.ToList<Mercenaries>();
            }
            else
            {
                GameCore.Camp.MercenariesList = new List<Mercenaries>();
            }

            if (swapCamp.tempWorldMapNodeList != null)
            {
                GameCore.Camp.tempWorldMapNodeList = swapCamp.tempWorldMapNodeList;
                GameCore.Camp.worldMapNodeList = GameCore.Camp.tempWorldMapNodeList.ToList<Node>();
            }
            else
            {
                GameCore.Camp.worldMapNodeList= new List<Node>();
            }

        }
        else
        {
            Debug.Log("File Not Exist not able to load LOL");
            Debug.Log("Create New Save File");

            GameCore.Camp = new CampSystem();

            savePlayerFile();
        }
    }
}

public class Gear
{
    public int GearType = 0; //0 = defult 1=頭部 2=身體 3=腳部 4=信物

    public float durability = 100;
    public float MaxDurability = 100;

    public float gearAdditionCharacterStrength = 0;
    public float gearAdditionCharacterEther = 0;
    public float gearAdditionCharacterAgility = 0;
    public float gearAdditionCharacterMentle = 0;
    public void gearFunction()
    {

    }
}

[System.Serializable]
public class Item
{
    public float ItemType = 0;
    ///
    /// 0 = Defult 無用途
    /// 1 = 攻擊性武器
    ///
    public float ItemSort = -1;// -1 =無效道具

    public float ItemAmount = 0;
    public float ItemMaxmentStackAmount = 64;
}

[System.Serializable]
public class weapon
{
    public float weaponName;
    public float weaponBasicDamage = 0;
    public string weaponType = "Defult Weapon Type";
}

[System.Serializable]
public class ViolentEnergyGear
{
    public float violentGearType = 0;
    public int violentGearSort = 0;
    public float violentGearRadius = 0;
    public float violentGearFunctionNumber = 0;

    //遊戲開始後 依照type/sort 給予按鈕dalegate
    public void propotyGive()
    {

    }
    public void sniper()
    {

    }
}

[System.Serializable]
public class chunk
{
        float anyway;
}